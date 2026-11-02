using Labs.Calculations;
using Labs.Tasks;
using Labs.Utils;
using Labs.Validation;

namespace Labs.Tests
{
    public class RegressionTests
    {
        [Test]
        public void FormulaHandlesMinimumIntegerDividedByMinusOne()
        {
            double dResult = Task1Calculator.Calculate(-1, int.MinValue, 1);
            Assert.That(dResult, Is.EqualTo(0));
        }

        [TestCase(3, 7, 4, 0.5)]
        [TestCase(3, -7, 4, -0.5)]
        [TestCase(-3, 7, 4, 0.5)]
        [TestCase(int.MinValue, int.MinValue, 1, 0)]
        public void FormulaPreservesRemainderSign(int iX, int iY, int iZ, double dExpected)
        {
            Assert.That(Task1Calculator.Calculate(iX, iY, iZ), Is.EqualTo(dExpected));
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, -1)]
        public void FormulaStillRejectsInvalidValues(int iX, int iZ)
        {
            Assert.Throws<ValidationException>(() => Task1Calculator.Calculate(iX, 1, iZ));
        }

        [TestCase("--task")]
        [TestCase("-s1")]
        [TestCase("-s2")]
        [TestCase("-1")]
        [TestCase("")]
        public void ParserTreatsSecondStringAsValue(string sValue)
        {
            CmdArgsParser parser = new CmdArgsParser(new[] { "--task", "3", "-s1", "abc", "-s2", sValue });
            Assert.That(parser.GetTaskNumber(1, 3), Is.EqualTo(3));
            Assert.That(parser.GetString("-s1", "S1"), Is.EqualTo("abc"));
            Assert.That(parser.GetString("-s2", "S2"), Is.EqualTo(sValue));
        }

        [Test]
        public void ParserPreservesOptionNamesInBothStringsAndAcceptsAnyOrder()
        {
            CmdArgsParser parser = new CmdArgsParser(new[] { "-s2", "-s1", "-s1", "--task", "--task", "3" });
            Assert.That(parser.GetTaskNumber(1, 3), Is.EqualTo(3));
            Assert.That(parser.GetString("-s1", "S1"), Is.EqualTo("--task"));
            Assert.That(parser.GetString("-s2", "S2"), Is.EqualTo("-s1"));
        }

        [Test]
        public void ParserPreservesRepeatedNegativeNumbers()
        {
            CmdArgsParser parser = new CmdArgsParser(new[] { "--task", "1", "-x", "-1", "-y", "-1", "-z", "1" });
            Assert.That(parser.GetInt("-x", "X"), Is.EqualTo(-1));
            Assert.That(parser.GetInt("-y", "Y"), Is.EqualTo(-1));
            Assert.That(parser.GetInt("-z", "Z"), Is.EqualTo(1));
        }

        [Test]
        public void ParserRejectsMissingStringValue()
        {
            CmdArgsParser parser = new CmdArgsParser(new[] { "--task", "3", "-s1", "abc", "-s2" });
            InputException? exError = Assert.Throws<InputException>(() => parser.GetString("-s2", "S2"));
            Assert.That(exError!.Message, Is.EqualTo("Value S2 must be specified."));
        }

        [TestCase("test@example.com\n")]
        [TestCase("test@example.com\r\n")]
        [TestCase("\ntest@example.com")]
        public void EmailRejectsExtraLineBreaks(string sValue)
        {
            Assert.Throws<ValidationException>(() => Task3StringChecker.ValidateEmail(sValue));
        }

        [TestCase("192.168.0.1\n")]
        [TestCase("192.168.0.1\r\n")]
        [TestCase("\n192.168.0.1")]
        public void IpRejectsExtraLineBreaks(string sValue)
        {
            Assert.Throws<ValidationException>(() => Task3StringChecker.ValidateIp(sValue));
        }

        [TestCase("123-456-7890\n")]
        [TestCase("123-456-7890\r\n")]
        [TestCase("\n123-456-7890")]
        public void PhoneRejectsExtraLineBreaks(string sValue)
        {
            Assert.Throws<ValidationException>(() => Task3StringChecker.ValidatePhone(sValue));
        }

        [Test]
        public void TypeChecksStillAcceptValidValues()
        {
            Assert.DoesNotThrow(() => Task3StringChecker.ValidateEmail("test@example.com"));
            Assert.DoesNotThrow(() => Task3StringChecker.ValidateIp("192.168.0.1"));
            Assert.DoesNotThrow(() => Task3StringChecker.ValidatePhone("123-456-7890"));
        }

        [Test]
        public void StringTaskTurnsValidationErrorsIntoFalse()
        {
            LabTask task = new StringCheckTask("test@example.com\n", "192.168.0.1\n");
            IReadOnlyList<string> lines = task.Execute();
            Assert.That(lines, Has.Count.EqualTo(9));
            Assert.That(lines[3], Is.EqualTo("S1_EMAIL: FALSE"));
            Assert.That(lines[8], Is.EqualTo("S2_IP: FALSE"));
        }
    }
}

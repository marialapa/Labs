using Labs.Calculations;
using Labs.Tasks;
using Labs.Validation;

namespace Labs.Tests
{
    public class LabTaskTests
    {
        [Test]
        public void FormulaTaskReturnsFormattedResult()
        {
            LabTask task = new FormulaTask(3, 7, 4);

            Assert.That(task.Execute(), Is.EqualTo(new[] { "RESULT: 0.500" }));
        }

        [TestCase(0, 4, "Value X must not be 0.")]
        [TestCase(3, 0, "Value Z must not be 0.")]
        [TestCase(3, -4, "Value Z must not be negative.")]
        public void FormulaTaskReportsInvalidInput(int iX, int iZ, string sMessage)
        {
            LabTask task = new FormulaTask(iX, 7, iZ);

            Assert.That(() => task.Execute(), Throws.TypeOf<ValidationException>().With.Message.EqualTo(sMessage));
        }

        [TestCase(2, 5, 4, 7, "DAYS: 2", "RESULT: 3.000")]
        [TestCase(2, 5, 5, 7, "DAYS: 1", "RESULT: 1.000")]
        [TestCase(2, 5, 6, 7, "DAYS: 0", "RESULT: 0.000")]
        public void DateTaskCountsInclusiveIntersection(
            int iFirstStart, int iFirstEnd, int iSecondStart, int iSecondEnd, string sDays, string sResult)
        {
            LabTask task = new DateIntersectionTask(
                new DateTime(2020, 1, iFirstStart), new DateTime(2020, 1, iFirstEnd),
                new DateTime(2020, 1, iSecondStart), new DateTime(2020, 1, iSecondEnd));

            Assert.That(task.Execute(), Is.EqualTo(new[] { sDays, sResult }));
        }

        [TestCase(2, 1, 1, 2, "first")]
        [TestCase(1, 2, 2, 1, "second")]
        public void DateTaskRejectsInvertedSegment(
            int iFirstStart, int iFirstEnd, int iSecondStart, int iSecondEnd, string sSegment)
        {
            LabTask task = new DateIntersectionTask(
                new DateTime(2020, 1, iFirstStart), new DateTime(2020, 1, iFirstEnd),
                new DateTime(2020, 1, iSecondStart), new DateTime(2020, 1, iSecondEnd));

            Assert.That(() => task.Execute(), Throws.TypeOf<ValidationException>()
                .With.Message.EqualTo($"End of the {sSegment} segment must not be earlier than its start."));
        }

        [Test]
        public void DateTaskRejectsIntersectionBeyondLimit()
        {
            DateTime dtStart = new DateTime(2000, 1, 1);
            DateTime dtEnd = dtStart.AddDays(10000);
            LabTask task = new DateIntersectionTask(dtStart, dtEnd, dtStart, dtEnd);

            Assert.That(() => task.Execute(), Throws.TypeOf<ValidationException>()
                .With.Message.EqualTo("Intersection length N = 10001 exceeds the limit of 10000 days."));
        }

        [TestCase(0, 0L)]
        [TestCase(4, 10L)]
        [TestCase(10000, 50005000L)]
        public void DateCalculatorSumsDaysUpToLimit(int iDays, long lExpected)
        {
            Assert.That(Task2Calculator.Calculate(iDays), Is.EqualTo(lExpected));
        }

        [Test]
        public void DateCalculatorRejectsNegativeDays()
        {
            Assert.That(() => Task2Calculator.Calculate(-1), Throws.TypeOf<ValidationException>()
                .With.Message.EqualTo("Intersection length N must not be negative."));
        }

        [Test]
        public void StringTaskReturnsAllChecksInOrder()
        {
            LabTask task = new StringCheckTask("person@example.com", "192.168.0.1");

            Assert.That(task.Execute(), Is.EqualTo(new[]
            {
                "EQUAL: FALSE",
                "NORMALIZED: FALSE",
                "REVERSED: FALSE",
                "S1_EMAIL: TRUE",
                "S1_PHONE: FALSE",
                "S1_IP: FALSE",
                "S2_EMAIL: FALSE",
                "S2_PHONE: FALSE",
                "S2_IP: TRUE"
            }));
        }

        [Test]
        public void StringTaskReportsAllFailedChecks()
        {
            LabTask task = new StringCheckTask("hello", "world");

            Assert.That(task.Execute(), Is.EqualTo(new[]
            {
                "EQUAL: FALSE",
                "NORMALIZED: FALSE",
                "REVERSED: FALSE",
                "S1_EMAIL: FALSE",
                "S1_PHONE: FALSE",
                "S1_IP: FALSE",
                "S2_EMAIL: FALSE",
                "S2_PHONE: FALSE",
                "S2_IP: FALSE"
            }));
        }

        [Test]
        public void NormalizedComparisonAcceptsCaseAndWhitespaceDifferences()
        {
            Assert.DoesNotThrow(() => Task3StringChecker.ValidateNormalizedEqual("  Hello\t\tWORLD  ", "hello world"));
        }

        [Test]
        public void ExactComparisonRejectsCaseDifference()
        {
            Assert.That(() => Task3StringChecker.ValidateEqual("Hello", "hello"),
                Throws.TypeOf<ValidationException>().With.Message.EqualTo("Strings are not equal character by character."));
        }

        [Test]
        public void ReversedComparisonAcceptsReversedText()
        {
            Assert.DoesNotThrow(() => Task3StringChecker.ValidateReversed("abc", "cba"));
        }

        [Test]
        public void ReversedComparisonRejectsUnrelatedText()
        {
            Assert.That(() => Task3StringChecker.ValidateReversed("abc", "abd"),
                Throws.TypeOf<ValidationException>().With.Message.EqualTo("The first string is not the reverse of the second one."));
        }
    }
}

# Лабораторная работа 2, вариант 7: инструкция

**Задание:** `--task 2`. Даны два отрезка дат. Находим N — сколько дней они пересекаются (оба конца считаются). Результат — сумма натуральных чисел от 1 до N, она считается **рекурсивно**: S(0) = 0, S(n) = n + S(n − 1).
`--task 1` (формула из ЛР1, вариант 14: `(Y % X) / sqrt(Z)`) работает как раньше.

## Какие файлы изменились

| Файл | Статус | Что сделано |
|---|---|---|
| `src/Labs/Tasks/Task2Calculator.cs` | **новый** | `GetIntersectionDays` считает длину пересечения отрезков с концами включительно. `TryCalculate` проверяет ограничение на N. `SumNaturalNumbers` — рекурсивный метод для формулы. Константа `MaxDays = 10000` — предел для N. |
| `src/Labs/Program.cs` | изменён | Задание выбирается через `switch` (`RunTask1` / `RunTask2`), у каждого задания свой метод. Добавлен разбор дат `TryParseDateArg`: строго `ДД.ММ.ГГГГ` и только существующая дата. Вывод вынесен в отдельные методы `PrintHelp`, `PrintError`, `PrintDays`, `PrintResult`. Допустимый `--task` теперь 1–2. В справку добавлены аргументы `-d1st/-d1end/-d2st/-d2end`. Переменные названы в венгерской нотации (`iTask`, `dtFirstStart`, `lResult`, `sError`…). |
| `src/Labs/Utils/CmdArgsParser.cs` | исправлен | Если ключ встречался дважды, `Dictionary.Add` выбрасывал исключение, и программа падала. Например, `-x -1 -y -1`: значение `-1` парсер принимает за ключ, и он попадает в словарь два раза. Теперь используется индексатор, и последнее значение побеждает. |
| `src/Labs/Tasks/Task1Calculator.cs` | не менялся | — |

## Что проверяет программа (`--task 2`)

- Все четыре даты обязательны: `ERROR: Value D1ST is required.`
- Формат строго `ДД.ММ.ГГГГ`. Строки `1.09.2020`, `10.9.2020`, `10.10.20`, а также несуществующие даты `32.12.2020` и `31.02.2021` дают `ERROR: Value D1ST must be a valid date in format DD.MM.YYYY.`
- Конец отрезка не может быть раньше начала: `ERROR: End of the first|second segment must not be earlier than its start.`
- Если отрезки не пересекаются, N = 0 и RESULT = 0.000.
- **Ограничение N ≤ 10000.** При большем N выводится `ERROR: Intersection length N = 36525 exceeds the limit of 10000 days.` Почему такой предел: глубина рекурсии при нём безопасна для стека, а сумма (до 50 005 000) точно помещается в `long`. Даже при N = 9983 расчёт занимает около 0.04 с.

Пример:
```
Labs --task 2 -d1st 12.12.2020 -d1end 17.12.2020 -d2st 10.12.2020 -d2end 15.12.2020
DAYS: 4
RESULT: 10.000
```
В `Lab02.md` для этого примера указан RESULT 6.000, но это значение для варианта 1. Для варианта 7 правильно 1+2+3+4 = 10.

## Как запустить тесты

В репозитории есть автотесты приёмки — PowerShell-скрипты в `src/UAT.Labs/`. Нужны **.NET 10 SDK** и **PowerShell 7+** (`pwsh`). На macOS их можно поставить так: `brew install --cask dotnet-sdk powershell`.

1. Собрать проект (из корня репозитория):
   ```bash
   dotnet build src/Labs
   ```
2. Проверить ЛР2, вариант 7:
   ```bash
   pwsh src/UAT.Labs/RunCheckLab02.ps1 -Variant 7
   ```
3. Убедиться, что ЛР1 не сломалась (вариант 14):
   ```bash
   pwsh src/UAT.Labs/RunCheckLab01.ps1 -Variant 14
   ```

Скрипт сам найдёт сборку в `src/Labs/bin/Debug/net10.0/`. Другой путь можно передать через `-ExePath`. В конце он выводит `Итог: OK: …, ERR: …, WARN: …`. Тесты пройдены, если ERR = 0.

### На Windows

Нужен .NET 10 SDK. PowerShell ставить не надо: скрипты работают и во встроенном Windows PowerShell 5.1. Команды вводить в PowerShell из корня репозитория:

```powershell
dotnet build src\Labs
powershell -ExecutionPolicy Bypass -File src\UAT.Labs\RunCheckLab02.ps1 -Variant 7
powershell -ExecutionPolicy Bypass -File src\UAT.Labs\RunCheckLab01.ps1 -Variant 14
```

`-ExecutionPolicy Bypass` нужен потому, что Windows по умолчанию не даёт запускать неподписанные скрипты. Если установлен PowerShell 7, вместо `powershell` можно писать `pwsh`. Скрипт сам найдёт `src\Labs\bin\Debug\net10.0\Labs.exe`.

Ручной запуск:
```bash
dotnet run --project src/Labs -- --task 2 -d1st 01.01.2020 -d1end 10.01.2020 -d2st 01.01.2020 -d2end 10.01.2020
```

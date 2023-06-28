# GITHUB


###  Основы программирования -------------------

#Lesson_01
<Переменные>

Попрактикуйтесь в создании переменных, 
объявить 10 переменных разных типов. 
Напоминание: переменные именуются с маленькой буквы, 
если название состоит из нескольких слов, 
то комбинируем их следующим образом - названиеПеременной. 
Также имя всегда должно отражать суть того, что хранит переменная. 
Для сдачи ДЗ требуется сдать код, 
который вы можете загрузить на https://gist.github.com/ 
или https://pastebin.com/ 
Это не сайт https://github.com/ где надо будет разбираться с работой git, 
а те сайты, на которые можно скопировать код

#Lesson_02
<Что выведется в консоль и почему?>

int a = 10;
int b = 38;
int c = (31 – 5 * a) / b;
Console.WriteLine(c);

ВАЖНО!!! Не запускать код и попытаться подумать головой. 
Также надо написать ответ “Почему?”

Приоритет арифметических операций:
В скобках сначала выполнится умножение 5 * 10 = 50,
далее вычитание 31-50 = -19,
и деление результата из скобок -19/38 = 0
Почему 0? Потому что тип переменной с это int - целочисленный, 
и все значения после запятой игнорируются/пропускаются

#Lesson_03
<Работа со строками>

Вы задаете вопросы пользователю, 
по типу "как вас зовут", 
"какой ваш знак зодиака" и тд, 
после чего, по данным, которые он ввел, 
формируете небольшой текст о пользователе. 
"Вас зовут Алексей, вам 21 год, вы водолей и работаете на заводе."

#Lesson_04
<Картинки>

На экране, в специальной зоне, выводятся картинки, 
по 3 в ряд (условно, ничего рисовать не надо). 
Всего у пользователя в альбоме 52 картинки. 
Код должен вывести, сколько полностью заполненных рядов 
можно будет вывести, и сколько картинок будет сверх меры.

В качестве решения ожидаются объявленные переменные 
с необходимыми значениями и, основываясь на значениях переменных, 
вывод необходимых данных. 
По задаче требуется выполнить простые математические действия.

#Lesson_05
<Перстановка местами значений>

Даны две переменные. 
Поменять местами значения двух переменных. 
Вывести на экран значения переменных до перестановки и после.
К примеру, есть две переменные имя и фамилия, 
они сразу инициализированные, но данные не верные, перепутанные. 
Вот эти данные и надо поменять местами через код.

#Lesson_06
<Магазин кристаллов>

Легенда:
Вы приходите в магазин и хотите купить за своё золото кристаллы. 
В вашем кошельке есть какое-то количество золота, 
продавец спрашивает у вас, 
сколько кристаллов вы хотите купить? 
После сделки у вас остаётся какое-то количество золота 
и появляется какое-то количество кристаллов.

Формально:
При старте программы пользователь вводит начальное количество золота. 
Потом ему предлагается купить какое-то количество 
кристаллов по цене N(задать в программе самому). 
Пользователь вводит число и его золото конвертируется в кристаллы. 
Остаток золота и кристаллов выводится на экран.

Проверять на то, что у игрока достаточно денег ненужно.

#Lesson_07
<Поликлиника>

Легенда:

Вы заходите в поликлинику и видите огромную очередь из старушек, вам нужно рассчитать время ожидания в очереди.

Формально:
Пользователь вводит кол-во людей в очереди.
Фиксированное время приема одного человека всегда равно 10 минутам.
Пример ввода: Введите кол-во старушек: 14
Пример вывода: "Вы должны отстоять в очереди 2 часа и 20 минут."


###  Условные операторы и циклы -------------------

#Lesson_08
<Освоение циклов>

При помощи циклов вы можете повторять один и тот же код множество раз.
Напишите простейшую программу, 
которая выводит указанное(установленное) 
пользователем сообщение заданное количество раз. 
Количество повторов также должен ввести пользователь.

#Lesson_09
<Контроль выхода>

Написать программу, которая будет выполняться до тех пор, пока не будет введено слово exit.
Помните, в цикле должно быть условие, которое отвечает за то, когда цикл должен завершиться.
Это нужно, чтобы любой разработчик взглянув на ваш код, понял четкие границы вашего цикла.

#Lesson_10
<Последовательность>

Нужно написать программу (используя циклы, обязательно пояснить выбор 
вашего цикла), чтобы она выводила следующую 
последовательность 5 12 19 26 33 40 47 54 61 68 75 82 89 96
Нужны переменные для обозначения чисел в условии цикла.

ОТВЕТ:
Выбрал цикл "for" так как есть четкое начало, конец и шаг итераций.
Т.е. для нас понятно когда цикл начнётся, когда закончится и с каким шагом будет выполняться.

#Lesson_11
<Сумма чисел>

С помощью Random получить число number, которое не больше 100. 
Найти сумму всех положительных чисел меньше number (включая число), 
которые кратные 3 или 5. 
К примеру, это числа 3, 5, 6, 9, 10, 12, 15 и т.д.)

#Lesson_12
<Конвертер валют>

Написать конвертер валют (3 валюты).

У пользователя есть баланс в каждой из представленных валют. 
Он может попросить сконвертировать часть баланса 
с одной валюты в другую. Тогда у него с баланса 
одной валюты снимется X и зачислится на баланс другой Y. 
Курс конвертации должен быть просто прописан в программе.
По имени переменной курса конвертации должно быть понятно, 
из какой валюты в какую валюту конвертируется.
Должна выполняться однотипная операция 
или везде умножение "*" 
или деление "/". 
Для чего это нужно подробнее позже узнаете в разделе "Функции". 
Но придётся объявить коэффициенты на все случаи.

Программа должна завершиться тогда, когда это решит пользователь.

Дополнительно: 
Если решение строится на switch, 
то принято работать с константами (в остальных случаях объявляются переменные). 
Для каждого case следует объявить константу.
Пример:
const string CommandExit = "exit";

case CommandExit:
break;

Константы объявляются перед блоком переменных и 
отделяются от них пустой строкой. 
Константы именуются с большой буквы. 
Если константа создана для связки 
консольное меню + switch (case) 
к имени константы добавляется Command или Menu 
- это передает суть константы, превращая ее в существительное, 
а не глагол и улучшает читаемость кода.

#Lesson_13
<Консольное меню>

При помощи всего, что вы изучили, создать приложение, 
которое может обрабатывать команды. 
Т.е. вы создаете меню, ожидаете ввода нужной команды, 
после чего выполняете действие, которое присвоено этой команде.

Примеры команд (требуется 4-6 команд, придумать самим):

SetName – установить имя;
ChangeConsoleColor- изменить цвет консоли;
SetPassword – установить пароль;
WriteName – вывести имя (после ввода пароля);
Esc – выход из программы.

Программа не должна завершаться после ввода, 
пользователь сам должен выйти из программы при помощи команды.


#Lesson_14
<Вывод имени>

Вывести имя в прямоугольник из символа, который введет сам пользователь.

Вы запрашиваете имя, после запрашиваете символ, 
а после отрисовываете в консоль его имя в прямоугольнике из его символов.

Пример:

Alexey

%

%%%%%%
% Alexey %
%%%%%%

Примечание:

Длину строки можно всегда узнать через свойство Length
string someString = “Hello”;
Console.WriteLine(someString.Length); //5

#Lesson_15
<Программа под паролем>

Создайте переменную типа string, 
в которой хранится пароль для доступа к тайному сообщению. 
Пользователь вводит пароль, 
далее происходит проверка пароля на правильность, 
и если пароль неверный, то попросите его ввести пароль ещё раз. 
Если пароль подошёл, выведите секретное сообщение.

Если пользователь неверно ввел пароль 3 раза, программа завершается

#Lesson_16
<Кратные числа>

Дано N (1 ≤ N ≤ 27). 
Найти количество трехзначных натуральных чисел, 
которые кратны N. 
Операции деления (/, %) не использовать. 
А умножение не требуется.
Число N всего одно, его надо получить в нужном диапазоне.

#Lesson_17
<Степень двойки>

Найдите минимальную степень двойки, превосходящую заданное число.
К примеру, для числа 4 будет 2 в степени 3, то есть 8. 4<8.
Для числа 29 будет 2 в степени 5, то есть 32. 29<32.
В консоль вывести число (лучше получить от Random), 
степень и само число 2 в найденной степени.


#Lesson_18
<Скобочное выражение>

Дана строка из символов '(' и ')'. 
Определить, является ли она корректным скобочным выражением. 
Определить максимальную глубину вложенности скобок.

Пример “(()(()))” - строка корректная и максимум глубины равняется 3.
Пример не верных строк: "(()", "())", ")(", "(()))(()"

Для перебора строки по символам можно использовать цикл foreach, 
к примеру будет так foreach (var symbol in text)
Или цикл for(int i = 0; i < text.Length; i++) и 
дальше обращаться к каждому символу внутри цикла как text[i]
Цикл нужен для перебора всех символов в строке.


#Lesson_19
<>

#Lesson_20
<>

#Lesson_21
<>

#Lesson_22
<>

#Lesson_23
<>

#Lesson_24
<>

#Lesson_25
<>

#Lesson_26
<>

#Lesson_27
<>

#Lesson_28
<>

#Lesson_29
<>

#Lesson_30
<>

#Lesson_31
<>








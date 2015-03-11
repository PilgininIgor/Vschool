using ILS.Domain.GameAchievements;

namespace ILS.Domain.Migrations
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;
	using ILS.Domain.QuestionGenerator;

	public sealed class Configuration : DbMigrationsConfiguration<ILS.Domain.ILSContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		static string CalculateSHA1(string text)
		{
			byte[] buffer = Encoding.Unicode.GetBytes(text);
			SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
			string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
			return hash;
		}

		protected override void Seed(ILS.Domain.ILSContext context)
		{
		    //context.Database.Delete();
            context.GameAchievements.Add(new GameAchievement
            {
                Name = "Первое посещение курса",
                AchievementExecutor = "ILS.Web.GameAchievements.VirtualWordAchievementExecutor",
                AchievementTrigger = AchievementTrigger.Game,
                Priority = 10
            });
		    return;
			var admin = context.Role.Add(new Role() { Name = "Admin" });
			var teacher = context.Role.Add(new Role() { Name = "Teacher" });
			var student = context.Role.Add(new Role() { Name = "Student" });
			context.User.Add(new User()
			{
				Name = "Alpha_Tester",
				PasswordHash = CalculateSHA1("Learning_in_3D"),
				Email = "sumrandommail@gmail.com",
				Roles = new List<Role> { teacher, student },
				IsApproved = true
			});
			context.User.Add(new User()
			{
				Name = "admin",
				PasswordHash = CalculateSHA1("password"),
				Email = "ihavemailtoo@gmail.com",
				Roles = new List<Role> { admin, teacher, student },
				IsApproved = true
			});

			context.User.Add(new User()
			{
				Name = "teacher2",
				PasswordHash = CalculateSHA1("password"),
				Roles = new List<Role> { teacher, student },
				IsApproved = false
			});
			context.User.Add(new User()
			{
				Name = "student1",
				PasswordHash = CalculateSHA1("password"),
				Roles = new List<Role> { student },
				IsApproved = false
			});

            #region EDucationAuthors
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Зеленко Лариса Сергеевна",
                Description = "Руководитель проекта, доцент кафедры программных систем СГАУ, кандидат технических наук. Основатель и идейный вдохновитель проекта, занимается разработкой обучающих программ и дистанционных систем управления, а так же внедрением новых образовательных и информационных технологий в учебный процесс. Имеет более 50 публикаций по данной тематике.",
                Image = "/ils2/Content/Sprites/authors/author_1.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Загуменнов Дмитрий",
                Description = "Главный архитектор системы, аспирант кафедры программных систем СГАУ, закончил СГАУ по специальности «Автоматизированные системы обработки информации и управления». В проекте с 2010 года, разработал архитектуру системы, исследовал технологии виртуальных миров, обосновал выбор технологии Unity, с помощью которой реализована система. Разработал модель данных системы и генератор виртуального обучающего пространства. Является дипломантом многих Международных и Всероссийских конкурсов и конференций, где представлял данную систему, имеет около 20 публикаций по данной тематике.",
                Image = "/ils2/Content/Sprites/authors/author_11.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Зинченко Алексей",
                Description = "Архитектор системы, выпускник СГАУ, закончил магистратуру по направлению «Фундаментальные информатика и информационные технологии». В проекте с 2011 года, разработал редактор учебных курсов, участвовал в разработке модели данных, исследовал технологии разработки мобильных приложений, реализовал мобильное приложение под ОС iOS. Является победителем проекта «У.М.Н.И.К.-2012» в команде участников. Имеет более 10 публикаций по данной тематике, выступал на Международных и Всероссийских конкурсах и конференциях, где его работы были отмечены дипломами и грамотами.",
                Image = "/ils2/Content/Sprites/authors/author_2.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Белов Константин",
                Description = "Главный разработчик системы, магистрант по направлению «Фундаментальные информатика и информационные технологии». В проекте с 2011 года, разработал систему анимирования персонажей, в настоящее время занимается разработкой  сетевой версии системы (клиентская часть), провел исследования и обосновал выбор сетевых технологий и протоколов обмена данными. Является победителем проекта «У.М.Н.И.К.-2012» в команде участников. Имеет более 15 публикаций по данной тематике, выступал на Международных и Всероссийских конкурсах и конференциях, где его работы были отмечены дипломами и грамотами.",
                Image = "/ils2/Content/Sprites/authors/author_9.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Петрухин Иван",
                Description = "Главный разработчик системы, магистрант по направлению «Фундаментальные информатика и информационные технологии». В проекте с 2011 года, разработал первых  персонажей системы, в настоящее время занимается разработкой  сетевой версии системы (серверная часть), провел исследования и обосновал выбор сетевых технологий и протоколов обмена данными. Является победителем проекта «У.М.Н.И.К.-2012» в команде участников. Имеет более 15 публикаций по данной тематике, выступал на Международных и Всероссийских конкурсах и конференциях, где его работы были отмечены дипломами и грамотами.",
                Image = "/ils2/Content/Sprites/authors/author_6.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Халитов Ильдар",
                Description = "Разработчик системы, магистрант по направлению «Фундаментальные информатика и информационные технологии». В проекте с 2010 года, разработал подсистему визуализации системы, исследовал шейдерные технологии, с помощью которых реализовано виртуальное пространство (лекционные комнаты, комнаты тестирования, обучающие локации) и все трехмерные объекты. Является победителем проекта «У.М.Н.И.К.-2012» в команде участников. Имеет более 10 публикаций по данной тематике, выступал на Международных и Всероссийских конкурсах и конференциях, где его работы были отмечены дипломами и грамотами.",
                Image = "/ils2/Content/Sprites/authors/author_8.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Григорьев Александр",
                Description = "",
                Image = "/ils2/Content/Sprites/authors/author_10.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Иванов Виталий",
                Description = "",
                Image = "/ils2/Content/Sprites/authors/author_3.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Конопелькин Дмитрий",
                Description = "",
                Image = "/ils2/Content/Sprites/authors/author_4.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Оськина Наталья",
                Description = "",
                Image = "/ils2/Content/Sprites/authors/author_5.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Семенов Александр",
                Description = "",
                Image = "/ils2/Content/Sprites/authors/author_7.jpg"
            });
            context.EDucationAuthor.Add(new EDucationAuthor()
            {
                Name = "Пученков Евгений",
                Description = "",
                Image = "/ils2/Content/Sprites/authors/author_12.jpg"
            });
            #endregion

            #region Informatics
            var course1 = new Course() { Name = "Информатика", Diagramm = Configuration.DefaultDiagramm }; context.Course.Add(course1);
			var theme1 = new Theme() { OrderNumber = 1, Name = "Подготовка к ЕГЭ" }; course1.Themes.Add(theme1);
			var lec1 = new Lecture() { OrderNumber = 1, Name = "Лекционный материал 1" }; theme1.ThemeContents.Add(lec1);
			var lec2 = new Lecture() { OrderNumber = 2, Name = "Лекционный материал 2" }; theme1.ThemeContents.Add(lec2);

			Paragraph paragraphNumericSystem;
			lec1.Paragraphs.Add(paragraphNumericSystem = new Paragraph() { OrderNumber = 1, Header = "1.1 Системы счисления", Text = "Система счисления — символический метод записи чисел, представление чисел с помощью письменных знаков. Cистема счисления дает представления множества чисел (целых и/или вещественных), дает каждому числу уникальное представление (или, по крайней мере, стандартное представление), отражает алгебраическую и арифметическую структуру чисел. Системы счисления подразделяются на позиционные, непозиционные и смешанные. Чем больше основание системы счисления, тем меньшее количество разрядов (то есть записываемых цифр) требуется при записи числа в позиционных системах счисления." });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 2, Header = "1.2 Кодирование информации", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 1.2" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 3, Header = "1.3 Структуры данных", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 1.3" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 4, Header = "2.1 Понятие алгоритма", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 2.1" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 5, Header = "2.2 Построение блок-схем", Text = "Схема — графическое представление определения, анализа или метода решения задачи, в котором используются символы для отображения операций, данных, потока, оборудования и т. д. Блок-схема — распространенный тип схем, описывающих алгоритмы или процессы, в которых отдельные шаги изображаются в виде блоков различной формы, соединенных между собой линиями." });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 6, Header = "2.3 Языки программирования", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 2.3" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 7, Header = "3.1 Логические выражения", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 3.1" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 8, Header = "3.2 Таблицы истинности", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 3.2" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 9, Header = "3.3 Введение в ИИ", Text = "Искусственный интеллект - наука и технология создания интеллектуальных машин, особенно интеллектуальных компьютерных программ. ИИ связан со сходной задачей использования компьютеров для понимания человеческого интеллекта, но не обязательно ограничивается биологически правдоподобными методами." });
			var p = new Paragraph() { OrderNumber = 10, Header = "4.1 Компьютерная графика", Text = "Недаром говорят: лучше один раз увидеть, чем сто раз услышать. Исследования подтверждают, что информационная пропускная способность органов зрения значительно выше, чем у других каналов передачи информации, доступных человеку. В теории информации доказано, что, подбрасывая монету и наблюдая результат, мы всякий раз получаем одну двоичную единицу (бит) информации. Каждая буква в тексте несет примерно 4 бита информации. Изображение участка поверхности Земли, полученное из космоса, содержит примерно 10 миллионов бит информации! Переработать такое количество информации под силу только самому современному компьютеру. Чтобы научить машину обрабатывать изображения, требуется иметь мощный комплекс технических средств, математический аппарат, алгоритмы и большое количество программ." };
			lec1.Paragraphs.Add(p);
			p.Pictures.Add(new Picture { OrderNumber = 1, Path = "http://localhost/ILS/Content/pics_test/ege-lec-01.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 2, Path = "http://localhost/ILS/Content/pics_test/ege-lec-02.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 3, Path = "http://localhost/ILS/Content/pics_test/ege-lec-03.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 4, Path = "http://localhost/ILS/Content/pics_test/ege-lec-04.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 5, Path = "http://localhost/ILS/Content/pics_test/ege-lec-05.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 6, Path = "http://localhost/ILS/Content/pics_test/ege-lec-06.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 7, Path = "http://localhost/ILS/Content/pics_test/ege-lec-07.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 8, Path = "http://localhost/ILS/Content/pics_test/ege-lec-08.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 9, Path = "http://localhost/ILS/Content/pics_test/ege-lec-09.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 10, Path = "http://localhost/ILS/Content/pics_test/ege-lec-10.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 11, Path = "http://localhost/ILS/Content/pics_test/ege-lec-11.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 12, Path = "http://localhost/ILS/Content/pics_test/ege-lec-12.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 13, Path = "http://localhost/ILS/Content/pics_test/ege-lec-13.jpg" });
			p.Pictures.Add(new Picture { OrderNumber = 14, Path = "http://localhost/ILS/Content/pics_test/ege-lec-14.jpg" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 11, Header = "4.2 Обработка изображений", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 4.2" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 12, Header = "4.3 Обзор графических редакторов", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 4.3" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 13, Header = "5.1 Электронные таблицы", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 5.1" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 14, Header = "5.2 Основные типы диаграмм", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 5.2" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 15, Header = "5.3 Работа с формулами в MS Excel", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 5.3" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 16, Header = "6.1 Архитектура ЭВМ", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 6.1" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 17, Header = "6.2 Центральный процессор", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 6.2" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 18, Header = "6.3 Основная память", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 6.3" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 19, Header = "7.1 Алгоритмы обработки данных - часть 1", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 7.1" });
			lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 20, Header = "7.2 Алгоритмы обработки данных - часть 2", Text = "Тут следует длинная строчка, вместившая в себе весь параграф 7.2" });
			lec2.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "Параграф 1", Text = "Текст параграфа 1" });
			lec2.Paragraphs.Add(new Paragraph() { OrderNumber = 2, Header = "Параграф 2", Text = "Текст параграфа 2" });
			lec2.Paragraphs.Add(new Paragraph() { OrderNumber = 3, Header = "Параграф 3", Text = "Текст параграфа 3" });
			lec2.Paragraphs.Add(new Paragraph() { OrderNumber = 4, Header = "Параграф 4", Text = "Текст параграфа 4" });
			lec2.Paragraphs.Add(new Paragraph() { OrderNumber = 5, Header = "Параграф 5", Text = "Текст параграфа 5" });

			//======================================================================================================================
			//======================================== ЕГЭ ДЕМО 2009 --- 9 ВОПРОСОВ ================================================
			//======================================================================================================================            

			var ege2009 = new Test() { OrderNumber = 3, Name = "ЕГЭ демо 2009", MinResult = 5 };
			theme1.ThemeContents.Add(ege2009);

			var ege2009_q = new Question()
			{
				OrderNumber = 1,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Автоматическое устройство осуществило перекодировку информационного сообщения на русском языке, первоначально записанного в 16-битном коде Unicode, в 8-битную кодировку КОИ-8. При этом информационное сообщение уменьшилось на 480 бит. Какова длина сообщения в символах?"
			};
			ege2009.Questions.Add(ege2009_q);
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "30", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "60", IfCorrect = true });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "120", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "480", IfCorrect = false });

			ege2009_q = new Question()
			{
				OrderNumber = 2,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "В велокроссе участвуют 119 спортсменов. Специальное устройство регистрирует прохождение каждым из участников промежуточного финиша, записывая его номер с использованием минимально возможного количества бит, одинакового для каждого спортсмена. Каков информационный объем сообщения, записанного устройством, после того как промежуточный финиш прошли 70 велосипедистов?"
			};
			ege2009.Questions.Add(ege2009_q);
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "70 бит", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "70 байт", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "490 бит", IfCorrect = true });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "119 байт", IfCorrect = false });

			ege2009_q = new Question()
			{
				OrderNumber = 3,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Дано: а = D7 (16-ричная система), b = 331 (8-ричная система). Какое из чисел c, записанных в двоичной системе, отвечает условию a<c<b?"
			};
			ege2009.Questions.Add(ege2009_q);
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "11011001", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "11011100", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "11010111", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "11011000", IfCorrect = true });

			ege2009_q = new Question()
			{
				OrderNumber = 4,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Чему равна сумма чисел 43 (8-ричная система) и 56 (16-ричная система)?"
			};
			ege2009.Questions.Add(ege2009_q);
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "121 (8-ричная система)", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "171 (8-ричная система)", IfCorrect = true });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "69 (16-ричная система)", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "1000001 (двоичная система)", IfCorrect = false });

			ege2009_q = new Question()
			{
				OrderNumber = 5,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Для кодирования букв А, Б, В, Г решили использовать двухразрядные последовательные двоичные числа (от 00 до 11, соответственно). Если таким способом закодировать последовательность символов БАВГ и записать результат шестнадцатеричным кодом, то получится..."
			};
			ege2009.Questions.Add(ege2009_q);
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "4B", IfCorrect = true });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "411", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "BACD", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "1023", IfCorrect = false });

			ege2009_q = new Question()
			{
				OrderNumber = 6,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2009-q6.jpg",
				IfPictured = false,
				PicA = null,
				Text = "Результаты тестирования представлены в таблице. Сколько записей в ней удовлетворяют условию\n«Пол=’ж’ ИЛИ Химия>Биология»?"
			};
			ege2009.Questions.Add(ege2009_q);
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "5", IfCorrect = true });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "2", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "3", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "4", IfCorrect = false });

			ege2009_q = new Question()
			{
				OrderNumber = 7,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Для кодирования цвета фона страницы Интернет используется атрибут bgcolor=\"#ХХХХХХ\", где в кавычках задаются шестнадцатеричные значения интенсивности цветовых компонент в 24-битной RGB-модели. Какой цвет будет у страницы, заданной тэгом <body bgcolor=\"#FFFFFF\">?"
			};
			ege2009.Questions.Add(ege2009_q);
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "белый", IfCorrect = true });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "зеленый", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "красный", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "синий", IfCorrect = false });

			ege2009_q = new Question()
			{
				OrderNumber = 8,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "В электронной таблице значение формулы =СУММ(B1:B2) равно 5. Чему равно значение ячейки B3, если значение формулы =СРЗНАЧ(B1:B3) равно 3?"
			};
			ege2009.Questions.Add(ege2009_q);
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "8", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "2", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "3", IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "4", IfCorrect = true });

			ege2009_q = new Question()
			{
				OrderNumber = 9,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2009-q9.jpg",
				IfPictured = true,
				PicA = "http://localhost/ILS/Content/pics_test/ege2009-a9.jpg",
				Text = "На диаграмме показано количество призеров олимпиады по информатике (И), математике (М), физике (Ф) в трех городах России. Какая из диаграмм правильно отражает соотношение общего числа призеров по каждому предмету для всех городов вместе?"
			};
			ege2009.Questions.Add(ege2009_q);
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = null, IfCorrect = true });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = null, IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = null, IfCorrect = false });
			ege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = null, IfCorrect = false });

			//======================================================================================================================
			//======================================== ЕГЭ ДЕМО 2010 --- 6 ВОПРОСОВ ================================================
			//======================================================================================================================            

			var ege2010 = new Test() { OrderNumber = 4, Name = "ЕГЭ демо 2010", MinResult = 3 };
			theme1.ThemeContents.Add(ege2010);

			var ege2010_q = new Question()
			{
				OrderNumber = 1,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Даны числа A и B, записанные в разных системах счисления: А = 9D (16-ричная система), B = 237 (8-ричная система). Какое из чисел С, записанных в двоичной системе, отвечает условию A<C<B?"
			};
			ege2010.Questions.Add(ege2010_q);
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "10011010", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "10011110", IfCorrect = true });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "10011111", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "11011110", IfCorrect = false });

			ege2010_q = new Question()
			{
				OrderNumber = 2,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "В некоторой стране автомобильный номер состоит из 7 символов. В качестве символов используют 18 различных букв и десятичные цифры в любом порядке. Каждый такой номер в компьютерной программе записывается минимально возможным и одинаковым целом количеством байтов, при этом используют посимвольное кодирование и все символы кодируются одинаковым и минимально возможным количеством битов. Определите объем памяти, отводимый этой программой для записи 60 номеров."
			};
			ege2010.Questions.Add(ege2010_q);
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "240 байт", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "300 байт", IfCorrect = true });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "360 байт", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "420 байт", IfCorrect = false });

			ege2010_q = new Question()
			{
				OrderNumber = 3,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2010-q3.jpg",
				IfPictured = false,
				PicA = null,
				Text = "На рисунке представлена часть кодовой таблицы ASCII. Каков шестнадцатеричный код символа q?"
			};
			ege2010.Questions.Add(ege2010_q);
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "71", IfCorrect = true });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "83", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "A1", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "B3", IfCorrect = false });

			ege2010_q = new Question()
			{
				OrderNumber = 4,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Вычислите сумму чисел X и Y. Результат представьте в двоичном виде. X = 110111 (двоичная система), Y = 135 (восьмеричная система)."
			};
			ege2010.Questions.Add(ege2010_q);
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "11010100", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "10100100", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "10010011", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "10010100", IfCorrect = true });

			ege2010_q = new Question()
			{
				OrderNumber = 5,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2010-q5.jpg",
				IfPictured = false,
				PicA = null,
				Text = "Определите значение переменной c после выполнения фрагмента программы, представленного на рисунке (фрагмент записан на разных языках программирования)."
			};
			ege2010.Questions.Add(ege2010_q);
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "c = 20", IfCorrect = true });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "c = 70", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "c = -20", IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "c = 180", IfCorrect = false });

			ege2010_q = new Question()
			{
				OrderNumber = 6,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2010-q6.jpg",
				IfPictured = true,
				PicA = "http://localhost/ILS/Content/pics_test/ege2010-a6.jpg",
				Text = "В программе используется одномерный целочисленный массив А с индексами от 0 до 10. На рисунке представлен фрагмент программы, записанный на разных языках программирования, в котором значения элементов сначала задаются, а затем меняются. Чему будут равны элементы этого массива после выполнения фрагмента программы?"
			};
			ege2010.Questions.Add(ege2010_q);
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = null, IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = null, IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = null, IfCorrect = false });
			ege2010_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = null, IfCorrect = true });

			//======================================================================================================================
			//======================================== ЕГЭ ДЕМО 2011 --- 15 ВОПРОСОВ ===============================================
			//======================================================================================================================            

			var ege2011 = new Test() { OrderNumber = 5, Name = "ЕГЭ демо 2011", MinResult = 8 };
			theme1.ThemeContents.Add(ege2011);

			var ege2011_q = new Question()
			{
				OrderNumber = 1,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Дано А = A71 (16-ричная система), B = 251 (8-ричная система). Какое из чисел C, записанных в двоичной системе, отвечает условию A<C<B?"
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "10101100", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "10101010", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "10101011", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "10101000", IfCorrect = true });

			ege2011_q = new Question()
			{
				OrderNumber = 2,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Автоматическое устройство осуществило перекодировку информационного сообщения на русском языке длиной в 20 символов, первоначально записанного в 16-битном коде Unicode, в 8-битную кодировку КОИ-8. На сколько при этом уменьшилось информационное сообщение?"
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "320 бит", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "20 бит", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "160 байт", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "20 байт", IfCorrect = true });

			ege2011_q = new Question()
			{
				OrderNumber = 3,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Для групповых операций с файлами используются маски имен файлов. Маска представляет собой последовательность букв, цифр и прочих допустимых в именах файлов символов, в которых также могут встречаться следующие: «?» означает ровно один произвольный символ; «*» означает любую последовательность символов произвольной длины,  в  том  числе пустую последовательность. Определите, по какой из масок будет выбрана указанная группа файлов: 1234.xls, 23.xml, 234.xls, 23.xml"
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "*23*.?x*", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "?23?.x??", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "?23?.x*", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "*23*.???", IfCorrect = true });

			ege2011_q = new Question()
			{
				OrderNumber = 4,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Чему равна сумма чисел 57 (8-ричная система) и 46 (16-ричная система)?"
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "351 (8-ричная система)", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "125 (8-ричная система)", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "55 (16-ричная система)", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "75 (16-ричная система)", IfCorrect = true });

			ege2011_q = new Question()
			{
				OrderNumber = 5,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Для передачи по каналу связи сообщения, состоящего только из символов А, Б, В и Г, используется неравномерный (по длине) код: А-00, Б-11, В-010, Г-011. Через канал связи передается сообщение: ГБВАВГ. Закодируйте сообщение данным кодом. Полученную двоичную последовательность переведите в шестнадцатеричную систему счисления. Какой вид будет иметь это сообщение?"
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "71013", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "DBCACD", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "7A13", IfCorrect = true });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "31A7", IfCorrect = false });

			ege2011_q = new Question()
			{
				OrderNumber = 6,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2011-q6.jpg",
				IfPictured = false,
				PicA = null,
				Text = "Путешественник пришел в 08:00 на автостанцию населенного пункта ЛИСЬЕ и обнаружил представленное на рисунке расписание автобусов для всей районной сети маршрутов. Определите самое раннее время, когда путешественник сможет оказаться в пункте ЗАЙЦЕВО согласно этому расписанию."
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "71013", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "DBCACD", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "7A13", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "31A7", IfCorrect = true });

			ege2011_q = new Question()
			{
				OrderNumber = 7,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Лена забыла пароль для входа в Windows XP, но помнила алгоритм его получения из символов «A153B42FB4» в строке подсказки. Если последовательность символов «В4» заменить на «B52» и из получившейся строки удалить все трехзначные числа, то полученная последовательность и будет паролем. Выберите верный пароль."
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "ABFB52", IfCorrect = true });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "AB42FB52", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "ABFB4", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "AB52FB", IfCorrect = false });

			ege2011_q = new Question()
			{
				OrderNumber = 8,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2011-q8.jpg",
				IfPictured = false,
				PicA = null,
				Text = "Определите значение переменной c после выполнения представленного на рисунке фрагмента программы, в котором a, b и с – переменные вещественного (действительного) типа."
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "c = 105", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "c = 160", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "c = 185", IfCorrect = true });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "c = 270", IfCorrect = false });

			ege2011_q = new Question()
			{
				OrderNumber = 9,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2011-q9.jpg",
				IfPictured = false,
				PicA = null,
				Text = "На рисунке дан фрагмент таблицы истинности выражения F. Какое выражение соответствует F?"
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "X /\\ ¬Y /\\ ¬Z", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "¬X /\\ ¬Y /\\ Z", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "¬X \\/ ¬Y \\/ Z", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "X \\/ ¬Y \\/ ¬Z", IfCorrect = true });

			ege2011_q = new Question()
			{
				OrderNumber = 10,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Укажите, какое логическое выражение равносильно выражению A \\/ ¬( ¬B \\/ ¬C)."
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "¬A \\/ B \\/ ¬C", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "A \\/ (B /\\ C)", IfCorrect = true });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "A \\/ B \\/ C", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "A \\/ ¬B \\/ ¬C", IfCorrect = false });

			ege2011_q = new Question()
			{
				OrderNumber = 11,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2011-q11.jpg",
				IfPictured = false,
				PicA = null,
				Text = "В динамической (электронной) таблице приведены значения посевных площадей (в га) и урожая (в центнерах) четырех зерновых культур в четырех хозяйствах одного района. В каком из хозяйств достигнута максимальная урожайность зерновых (по валовому сбору)? Урожайность измеряется в центнерах с гектара."
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Заря", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "Первомайское", IfCorrect = true });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "Победа", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "Рассвет", IfCorrect = false });

			ege2011_q = new Question()
			{
				OrderNumber = 12,
				PicQ = "http://localhost/ILS/Content/pics_test/ege2011-q12.jpg",
				IfPictured = false,
				PicA = null,
				Text = "Торговое предприятие владеет тремя магазинами (I, II и III), каждый из которых реализует периферийные компьютерные устройства: мониторы (М), принтеры (П), сканеры (С) или клавиатуры (К). На диаграмме 1 показано количество проданных товаров каждого вида за месяц. На диаграмме 2 показано, как за тот же период соотносятся продажи товаров (в штуках) в трех магазинах предприятия. Какое из утверждений следует из анализа обеих диаграмм?"
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Все сканеры могли быть проданы через магазин III", IfCorrect = true });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "Все принтеры и сканеры могли быть проданы через магазин II", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "Все мониторы могли быть проданы через магазин I", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "Ни один принтер не был продан через магазин II", IfCorrect = false });

			ege2011_q = new Question()
			{
				OrderNumber = 13,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Какое из приведенных имен удовлетворяет логическому условию:\n¬ (последняя буква гласная → первая буква согласная) /\\ вторая буква согласная"
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "ИРИНА", IfCorrect = true });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "АРТЕМ", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "СТЕПАН", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "МАРИЯ", IfCorrect = false });

			ege2011_q = new Question()
			{
				OrderNumber = 14,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "В некоторой стране автомобильный номер длиной 7 символов составляют из заглавных букв (используются только 22 различные буквы) и десятичных цифр в любом порядке. Каждый такой номер в компьютерной программе записывается минимально возможным и одинаковым целым количеством байт (при этом используют посимвольное кодирование и все символы кодируются одинаковым и минимально возможным количеством бит). Определите объем памяти, отводимый этой программой для записи 50 номеров."
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "350 байт", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "300 байт", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "250 байт", IfCorrect = true });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "200 байт", IfCorrect = false });

			ege2011_q = new Question()
			{
				OrderNumber = 15,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "В программе описан одномерный целочисленный массив A с индексами от 0 до 10. Ниже представлен фрагмент этой программы, записанный на разных языках программирования, в котором значения элементов массива сначала задаются, а затем меняются. Чему окажутся равны элементы этого массива?"
			};
			ege2011.Questions.Add(ege2011_q);
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "9 9 9 9 9 9 9 9 9 9 9", IfCorrect = true });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "0 1 2 3 4 5 6 7 8 9 9", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "0 1 2 3 4 5 6 7 8 9 10", IfCorrect = false });
			ege2011_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "-1 -1 0 1 2 3 4 5 6 7 8", IfCorrect = false });
			#endregion

			#region Informatics ENG
			var ecourse1 = new Course() { Name = "Information Science", Diagramm = Configuration.DefaultDiagramm }; context.Course.Add(ecourse1);
			var etheme1 = new Theme() { OrderNumber = 1, Name = "Exam preparation" }; ecourse1.Themes.Add(etheme1);
			var elec1 = new Lecture() { OrderNumber = 1, Name = "Lecturing material 1" }; etheme1.ThemeContents.Add(elec1);
			var elec2 = new Lecture() { OrderNumber = 2, Name = "Lecturing material 2" }; etheme1.ThemeContents.Add(elec2);

			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "1.1 Number systems", Text = "A numeral system (or system of numeration) is a writing system for expressing numbers, that is a mathematical notation for representing numbers of a given set, using graphemes or symbols in a consistent manner. It can be seen as the context that allows the symbols '11' to be interpreted as the binary symbol for three, the decimal symbol for eleven, or a symbol for other numbers in different bases." });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 2, Header = "1.2 Data encryption", Text = "Here comes a single but truly long line that encompasses all paragraph 1.2" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 3, Header = "1.3 Data structures", Text = "Here comes a single but truly long line that encompasses all paragraph 1.3" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 4, Header = "2.1 Definition of algorithm", Text = "Here comes a single but truly long line that encompasses all paragraph 2.1" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 5, Header = "2.2 Flowcharts", Text = "A flowchart is a type of diagram that represents an algorithm or process, showing the steps as boxes of various kinds, and their order by connecting these with arrows. This diagrammatic representation can give a step-by-step solution to a given problem. Process operations are represented in these boxes, and arrows connecting them represent flow of control. Data flows are not typically represented in a flowchart, in contrast with data flow diagrams; rather, they are implied by the sequencing of operations. Flowcharts are used in analyzing, designing, documenting or managing a process or program in various fields." });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 6, Header = "2.3 Programming languages", Text = "Here comes a single but truly long line that encompasses all paragraph 2.3" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 7, Header = "3.1 Logical expressions", Text = "Here comes a single but truly long line that encompasses all paragraph 3.1" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 8, Header = "3.2 Truth tables", Text = "Here comes a single but truly long line that encompasses all paragraph 3.2" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 9, Header = "3.3 Introduction to AI", Text = "Artificial intelligence (AI) is the intelligence of machines and the branch of computer science that aims to create it. AI textbooks define the field as 'the study and design of intelligent agents' where an intelligent agent is a system that perceives its environment and takes actions that maximize its chances of success. John McCarthy, who coined the term in 1956, defines it as 'the science and engineering of making intelligent machines.'" });
			var ep = new Paragraph() { OrderNumber = 10, Header = "4.1 Computer graphics", Text = "Computer graphics are graphics created using computers and, more generally, the representation and manipulation of image data by a computer with help from specialized software and hardware. The development of computer graphics has made computers easier to interact with, and better for understanding and interpreting many types of data. Developments in computer graphics have had a profound impact on many types of media and have revolutionized animation, movies and the video game industry." };
			elec1.Paragraphs.Add(ep);
			ep.Pictures.Add(new Picture { OrderNumber = 1, Path = "http://localhost/ILS/Content/pics_test/ege-lec-01.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 2, Path = "http://localhost/ILS/Content/pics_test/ege-lec-02.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 3, Path = "http://localhost/ILS/Content/pics_test/ege-lec-03.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 4, Path = "http://localhost/ILS/Content/pics_test/ege-lec-04.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 5, Path = "http://localhost/ILS/Content/pics_test/ege-lec-05.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 6, Path = "http://localhost/ILS/Content/pics_test/ege-lec-06.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 7, Path = "http://localhost/ILS/Content/pics_test/ege-lec-07.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 8, Path = "http://localhost/ILS/Content/pics_test/ege-lec-08.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 9, Path = "http://localhost/ILS/Content/pics_test/ege-lec-09.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 10, Path = "http://localhost/ILS/Content/pics_test/ege-lec-10.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 11, Path = "http://localhost/ILS/Content/pics_test/ege-lec-11.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 12, Path = "http://localhost/ILS/Content/pics_test/ege-lec-12.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 13, Path = "http://localhost/ILS/Content/pics_test/ege-lec-13.jpg" });
			ep.Pictures.Add(new Picture { OrderNumber = 14, Path = "http://localhost/ILS/Content/pics_test/ege-lec-14.jpg" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 11, Header = "4.2 Image processing", Text = "Here comes a single but truly long line that encompasses all paragraph 4.2" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 12, Header = "4.3 Popular graphic editors", Text = "Here comes a single but truly long line that encompasses all paragraph 4.3" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 13, Header = "5.1 Spreadsheet", Text = "Here comes a single but truly long line that encompasses all paragraph 5.1" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 14, Header = "5.2 Common types of diagrams", Text = "Here comes a single but truly long line that encompasses all paragraph 5.2" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 15, Header = "5.3 Expressions in MS Excel", Text = "Here comes a single but truly long line that encompasses all paragraph 5.3" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 16, Header = "6.1 Computer architecture", Text = "Here comes a single but truly long line that encompasses all paragraph 6.1" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 17, Header = "6.2 Central processing unit (CPU)", Text = "Here comes a single but truly long line that encompasses all paragraph 6.2" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 18, Header = "6.3 Main memory", Text = "Here comes a single but truly long line that encompasses all paragraph 6.3" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 19, Header = "7.1 Common algorithms - part 1", Text = "Here comes a single but truly long line that encompasses all paragraph 7.1" });
			elec1.Paragraphs.Add(new Paragraph() { OrderNumber = 20, Header = "7.2 Common algorithms - part 2", Text = "Here comes a single but truly long line that encompasses all paragraph 7.2" });
			elec2.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "Paragraph 1", Text = "Text of paragraph 1" });
			elec2.Paragraphs.Add(new Paragraph() { OrderNumber = 2, Header = "Paragraph 2", Text = "Text of paragraph 2" });
			elec2.Paragraphs.Add(new Paragraph() { OrderNumber = 3, Header = "Paragraph 3", Text = "Text of paragraph 3" });
			elec2.Paragraphs.Add(new Paragraph() { OrderNumber = 4, Header = "Paragraph 4", Text = "Text of paragraph 4" });
			elec2.Paragraphs.Add(new Paragraph() { OrderNumber = 5, Header = "Paragraph 5", Text = "Text of paragraph 5" });

			//======================================================================================================================
			//======================================== ЕГЭ ДЕМО 2009 --- 9 ВОПРОСОВ ================================================
			//======================================================================================================================            

			var eege2009 = new Test() { OrderNumber = 3, Name = "Exam demo 2009", MinResult = 5 };
			etheme1.ThemeContents.Add(eege2009);

			var eege2009_q = new Question()
			{
				OrderNumber = 1,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "An automatic device converted every symbol of a message from 16-bit Unicode into 8-bit KOI-8R. The length of the message decreased by 480 bits. How many symbols does the message contain?"
			};
			eege2009.Questions.Add(eege2009_q);
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "30", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "60", IfCorrect = true });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "120", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "480", IfCorrect = false });

			eege2009_q = new Question()
			{
				OrderNumber = 2,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "There are 119 participants of a cross-country race. When one of the participants passes the checkpoint, a special device writes down his number using minimum (but same for everyone) amouth of bits possible. How long would be the message registered by the device when 70 participants cross checkpoint?"

			};
			eege2009.Questions.Add(eege2009_q);
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "70 bits", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "70 bytes", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "490 bits", IfCorrect = true });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "119 bytes", IfCorrect = false });

			eege2009_q = new Question()
			{
				OrderNumber = 3,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Given: A = D7 (hexadecimal system), B = 331 (octal system). Choose C (binary system) that makes next inequation true: A<C<B"
			};
			eege2009.Questions.Add(eege2009_q);
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "11011001", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "11011100", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "11010111", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "11011000", IfCorrect = true });

			eege2009_q = new Question()
			{
				OrderNumber = 4,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "What is the sum of 43 (octal system) and 56 (hexadecimal system)?"
			};
			eege2009.Questions.Add(eege2009_q);
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "121 (octal system)", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "171 (octal system)", IfCorrect = true });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "69 (hexadecimal system)", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "1000001 (binary system)", IfCorrect = false });

			eege2009_q = new Question()
			{
				OrderNumber = 5,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Letters A, B, C, D are encrypted with 2-digit binary numbers (from 00 to 11 accordingly). Use this code to encrypt the next symbol sequence: BACD, then convert it into hexadecimal system."
			};
			eege2009.Questions.Add(eege2009_q);
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "4B", IfCorrect = true });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "411", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "BACD", IfCorrect = false });
			eege2009_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "1023", IfCorrect = false });
			#endregion

			#region LinkedInformatics
			var li_course = new Course() { Name = "Связанная Информатика", Diagramm = Configuration.DefaultDiagramm }; context.Course.Add(li_course);

			var li_theme1 = new Theme() { OrderNumber = 1, Name = "Начальная тема" }; li_course.Themes.Add(li_theme1);
			var li_theme2 = new Theme() { OrderNumber = 2, Name = "Математические основы" }; li_course.Themes.Add(li_theme2);
			var li_theme3 = new Theme() { OrderNumber = 3, Name = "Итоговая тема" }; li_course.Themes.Add(li_theme3);

			var li_lec1 = new Lecture() { OrderNumber = 1, Name = "Введение" }; li_theme1.ThemeContents.Add(li_lec1);
			var li_lec2 = new Lecture() { OrderNumber = 1, Name = "Системы счисления" }; li_theme2.ThemeContents.Add(li_lec2);

			li_lec1.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "1.1 Введение", Text = "Изучение информатики - чрезвычайно важный процесс" });
			li_lec2.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "1.1 Системы счисления", Text = "Система счисления — символический метод записи чисел, представление чисел с помощью письменных знаков. Cистема счисления дает представления множества чисел (целых и/или вещественных), дает каждому числу уникальное представление (или, по крайней мере, стандартное представление), отражает алгебраическую и арифметическую структуру чисел. Системы счисления подразделяются на позиционные, непозиционные и смешанные. Чем больше основание системы счисления, тем меньшее количество разрядов (то есть записываемых цифр) требуется при записи числа в позиционных системах счисления." });

			var li_test = new Test() { OrderNumber = 2, Name = "Математический тест", MinResult = 2 };
			li_theme2.ThemeContents.Add(li_test);

			var li_test_q = new Question()
			{
				OrderNumber = 1,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Корень десятой степени из 1024?"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "102.4", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "2", IfCorrect = true });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "1014", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "32", IfCorrect = false });

			li_test_q = new Question()
			{
				OrderNumber = 2,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "2 * (40 - 20 / 2) + 4 / 2"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "32", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "62", IfCorrect = true });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "12", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "22", IfCorrect = false });


			li_test_q = new Question()
			{
				OrderNumber = 3,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Найдите корни уравнения: x^2 - 4x - 5 = 0"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "3", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "5", IfCorrect = true });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "2", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "-1", IfCorrect = true });


			li_test = new Test() { OrderNumber = 2, Name = "Тест по информатике", MinResult = 2 };
			li_theme3.ThemeContents.Add(li_test);

			li_test_q = new Question()
			{
				OrderNumber = 1,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Бит это:"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Основная единица измерения количества информации", IfCorrect = true });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "Не знаю", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "Нет такого слова", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "Да", IfCorrect = false });

			li_test_q = new Question()
			{
				OrderNumber = 2,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Что из перечисленного - не язык программирования?"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Java", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "C#", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "C++", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "Coffee", IfCorrect = true });


			li_test_q = new Question()
			{
				OrderNumber = 3,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Какой из эти алгоритмов - алгоритм сортировки?"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Алгоритм Форда — Фалкерсона", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "Алгоритм Малхотры — Кумара — Махешвари", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "Алгоритм Робинсона — Шенстеда", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "Алгоритм Шелла", IfCorrect = true });



			li_test = new Test() { OrderNumber = 3, Name = "Составной тест", MinResult = 4, IsComposite = true };
			li_theme3.ThemeContents.Add(li_test);

			li_test_q = new Question()
			{
				OrderNumber = 6,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Бит это:"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Основная единица измерения количества информации", IfCorrect = true });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "Не знаю", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "Нет такого слова", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "Да", IfCorrect = false });

			li_test_q = new Question()
			{
				OrderNumber = 1,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Что из перечисленного - не язык программирования?"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Java", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "C#", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "C++", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "Coffee", IfCorrect = true });


			li_test_q = new Question()
			{
				OrderNumber = 2,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Какой из эти алгоритмов - алгоритм сортировки?"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Алгоритм Форда — Фалкерсона", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "Алгоритм Малхотры — Кумара — Махешвари", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "Алгоритм Робинсона — Шенстеда", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "Алгоритм Шелла", IfCorrect = true });

			li_test_q = new Question()
			{
				OrderNumber = 4,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Корень десятой степени из 1024?"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "102.4", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "2", IfCorrect = true });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "1014", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "32", IfCorrect = false });

			li_test_q = new Question()
			{
				OrderNumber = 3,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "2 * (40 - 20 / 2) + 4 / 2"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "32", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "62", IfCorrect = true });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "12", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "22", IfCorrect = false });


			li_test_q = new Question()
			{
				OrderNumber = 5,
				PicQ = null,
				IfPictured = false,
				PicA = null,
				Text = "Найдите корни уравнения: x^2 - 4x - 5 = 0"
			};
			li_test.Questions.Add(li_test_q);
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "3", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "5", IfCorrect = true });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "2", IfCorrect = false });
			li_test_q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "-1", IfCorrect = true });



			ThemeLink tl1 = context.ThemeLink.Add(new ThemeLink() { ParentTheme = li_theme1, LinkedTheme = li_theme2 });
			li_theme1.OutputThemeLinks.Add(tl1);
			ThemeLink tl2 = context.ThemeLink.Add(new ThemeLink() { ParentTheme = li_theme2, LinkedTheme = li_theme3 });
			li_theme2.OutputThemeLinks.Add(tl2);

			#endregion

			#region QuestionGenerator
//			// ========================== СЛОЖНОСТИ ЗАДАНИЙ =======================================
//			var simpleLevel = context.Level.Add(new Level() { Description = "Простой" });
//			var middleLevel = context.Level.Add(new Level() { Description = "Средний" });
//			var hardLevel = context.Level.Add(new Level() { Description = "Сложный" });
//
//			// ========================== ТИПЫ ЗАДАНИЙ ============================================
//			var multiplyChoisesSingleCorrectAnswer = context.TypeOfQuestion.Add(
//				new TypeOfQuestion()
//				{
//					TypeOfQuestionIdentifier = "MultiplyChoisesSingleCorrectAnswer",
//					Description = "Несколько вариантов ответов - 1 верный (часть А ЕГЭ)"
//				});
//			var freeAnswer = context.TypeOfQuestion.Add(
//				new TypeOfQuestion()
//				{
//					TypeOfQuestionIdentifier = "FreeAnswer",
//					Description = "Свой ответ (часть В ЕГЭ)"
//				});
//
//			// ========================== ШАБЛОНЫ ЗАДАНИЙ =========================================
//			var template1 = context.QuestionTemplate.Add(new QuestionTemplate()
//			{
//				Description = "Перевод десятичного числа в 2-ю СС, подсчет кол-ва нулей/единиц в двоичном представлении числа.",
//				ClassName = "NumeralSystems.NumSys_Type1"
//			});
//
//			// ========================== ПАРАМЕТРЫ ===============================================
//			var numSys_Type1_CenterGenerateNumber = context.Parameter.Add(new Parameter()
//			{
//				ParameterIdentifier = "NumSys_Type1_CenterGenerateNumber",
//				Description = "Центр генерируемого числа",
//				QuestionTemplate = template1
//			});
//			var numSys_Type1_RadiusGenerateNumber = context.Parameter.Add(new Parameter()
//			{
//				ParameterIdentifier = "NumSys_Type1_RadiusGenerateNumber",
//				Description = "Максимальный радиус разброса генерируемого числа",
//				QuestionTemplate = template1
//			});
//
//			// ========================== ОГРАНИЧЕНИЯ НА ПАРАМЕТРЫ ================================
//			context.Constraint.Add(new Constraint()
//			{
//				Parameter = numSys_Type1_CenterGenerateNumber,
//				TypeOfConstraint = "GreaterThanOrEqual",
//				Value = 10
//			});
//			context.Constraint.Add(new Constraint()
//			{
//				Parameter = numSys_Type1_CenterGenerateNumber,
//				TypeOfConstraint = "LessThanOrEqual",
//				Value = 10000
//			});
//			context.Constraint.Add(new Constraint()
//			{
//				Parameter = numSys_Type1_RadiusGenerateNumber,
//				TypeOfConstraint = "GreaterThanOrEqual",
//				Value = 5
//			});
//			context.Constraint.Add(new Constraint()
//			{
//				Parameter = numSys_Type1_RadiusGenerateNumber,
//				TypeOfConstraint = "LessThanOrEqual",
//				Value = 5000
//			});
//
//			// ========================== ЗНАЧЕНИЯ ПАРАМЕТРОВ =====================================
//			context.ParameterValue.Add(new ParameterValue()
//			{
//				Level = simpleLevel,
//				Parameter = numSys_Type1_CenterGenerateNumber,
//				Value = "128"
//			});
//			context.ParameterValue.Add(new ParameterValue()
//			{
//				Level = simpleLevel,
//				Parameter = numSys_Type1_RadiusGenerateNumber,
//				Value = "32"
//			});
//			context.ParameterValue.Add(new ParameterValue()
//			{
//				Level = middleLevel,
//				Parameter = numSys_Type1_CenterGenerateNumber,
//				Value = "512"
//			});
//			context.ParameterValue.Add(new ParameterValue()
//			{
//				Level = middleLevel,
//				Parameter = numSys_Type1_RadiusGenerateNumber,
//				Value = "128"
//			});
//			context.ParameterValue.Add(new ParameterValue()
//			{
//				Level = hardLevel,
//				Parameter = numSys_Type1_CenterGenerateNumber,
//				Value = "4096"
//			});
//			context.ParameterValue.Add(new ParameterValue()
//			{
//				Level = hardLevel,
//				Parameter = numSys_Type1_RadiusGenerateNumber,
//				Value = "512"
//			});
//
//			// ========================== ГЕНЕРИРУЕМЫЙ ТЕСТ =======================================
//			var test1 = context.GeneratedTest.Add(new GeneratedTest() { Paragraph = paragraphNumericSystem });
//
//			// ========================== ЭКЗЕМПЛЯРЫ ЗАДАНИЙ ======================================
//			var instanceOfQuestion1 = context.InstanceOfQuestion.Add(new InstanceOfQuestion()
//			{
//				Level = middleLevel,
//				QuestionTemplate = template1,
//				TypeOfQuestion = multiplyChoisesSingleCorrectAnswer
//			});
//
//			// ========================== СОДЕРЖИМОЕ ТЕСТА ========================================
//			var testContent1 = context.TestContent.Add(new TestContent()
//			{
//				GeneratedTest = test1,
//				OrderNumber = 1,
//				InstanceOfQuestion = instanceOfQuestion1
//			});
			#endregion

			#region Math
			/*var course2 = new Course() { Name = "Математика", Diagramm = ILSContextInitializer.DefaultDiagramm }; context.Course.Add(course2);
            var theme21 = new Theme() { OrderNumber = 1, Name = "Дифференцирование" }; course2.Themes.Add(theme21);
            var theme22 = new Theme() { OrderNumber = 2, Name = "Интегрирование" }; course2.Themes.Add(theme22);
            var theme23 = new Theme() { OrderNumber = 3, Name = "Тригонометрия" }; course2.Themes.Add(theme23);
            var lec211 = new ThemeContent() { OrderNumber = 1, Name = "Лекция 11", Type = "lecture" }; theme21.ThemeContents.Add(lec211);
            var lec212 = new ThemeContent() { OrderNumber = 2, Name = "Лекция 12", Type = "lecture" }; theme21.ThemeContents.Add(lec212);
            var lec221 = new ThemeContent() { OrderNumber = 1, Name = "Лекция 21", Type = "lecture" }; theme22.ThemeContents.Add(lec221);
            var lec222 = new ThemeContent() { OrderNumber = 2, Name = "Лекция 22", Type = "lecture" }; theme22.ThemeContents.Add(lec222);
            var lec231 = new ThemeContent() { OrderNumber = 1, Name = "Лекция 31", Type = "lecture" }; theme23.ThemeContents.Add(lec231);
            var lec232 = new ThemeContent() { OrderNumber = 2, Name = "Лекция 32", Type = "lecture" }; theme23.ThemeContents.Add(lec232);
            lec211.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "Увлекательный параграф", Text = "Текст увлекательного параграфа" });
            lec212.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "Внушительный параграф", Text = "Текст внушительного параграфа" });
            lec221.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "Неожиданный параграф", Text = "Текст неожиданного параграфа" });
            lec222.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "Впечатляющий параграф", Text = "Текст впечатляющего параграфа" });
            lec231.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "Первый замечательный параграф", Text = "Текст первого замечательного параграфа" });
            lec231.Paragraphs.Add(new Paragraph() { OrderNumber = 2, Header = "Второй замечательный параграф", Text = "Текст второго замечательного параграфа" });
            lec232.Paragraphs.Add(new Paragraph() { OrderNumber = 1, Header = "Запредельный параграф", Text = "Текст запредельного параграфа" }); */
			#endregion

			#region Physics
			/*var course3 = new Course() { Name = "Физика", Diagramm = ILSContextInitializer.DefaultDiagramm }; context.Course.Add(course3);
            var theme3 = new Theme() { OrderNumber = 1, Name = "Механика" }; course3.Themes.Add(theme3);
            
            var test1 = new ThemeContent() { OrderNumber = 1, Name = "Тест (1)", Type = "test", MinResult = 5 };
            theme3.ThemeContents.Add(test1);
            for (var i = 1; i < 10; i++)
            {
                var q = new Question() { OrderNumber = i, Text = "Вопрос " + i }; test1.Questions.Add(q);
                q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Ответ " + i + "1", IfCorrect = false });
                q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "Ответ " + i + "2", IfCorrect = true });
                q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "Ответ " + i + "3", IfCorrect = false });
            }

            var test2 = new ThemeContent() { OrderNumber = 2, Name = "Тест (2)", Type = "test", MinResult = 5 };
            theme3.ThemeContents.Add(test2);
            for (var i = 1; i < 10; i++)
            {
                var q = new Question() { OrderNumber = i, Text = "Вопрос " + i }; test2.Questions.Add(q);
                q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 1, Text = "Ответ " + i + "1", IfCorrect = false });
                q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 2, Text = "Ответ " + i + "2", IfCorrect = true });
                q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 3, Text = "Ответ " + i + "3", IfCorrect = true });
                q.AnswerVariants.Add(new AnswerVariant() { OrderNumber = 4, Text = "Ответ " + i + "4", IfCorrect = false });
			}*/
			#endregion

            #region GameAchievements
            context.GameAchievements.Add(new GameAchievement
            {
                Name = "Первое посещение курса",
                AchievementExecutor = "ILS.Web.GameAchievements.VirtualWordAchievementExecutor",
                AchievementTrigger = AchievementTrigger.Game,
                Priority = 10
            });
            #endregion
        }


		static string DefaultDiagramm = @"[{'dx':0,'dy':0,'rot':0,'sx':1,'sy':1,'module':'fsa','object':'State','position':{'x':300,'y':50},'label':'state 2','radius':30,'labelOffsetX':15,'labelOffsetY':23,'attrs':{'fill':'white'},'euid':2},{'dx':0,'dy':0,'rot':0,'sx':1,'sy':1,'module':'fsa','object':'State','position':{'x':120,'y':120},'label':'state 1','radius':30,'labelOffsetX':15,'labelOffsetY':23,'attrs':{'fill':'white'},'euid':3},{'dx':0,'dy':0,'rot':0,'sx':1,'sy':1,'module':'fsa','object':'EndState','position':{'x':450,'y':150},'radius':10,'innerRadius':5,'attrs':{'fill':'white'},'innerAttrs':{'fill':'black'},'euid':4},{'dx':0,'dy':0,'rot':0,'sx':1,'sy':1,'module':'fsa','object':'StartState','position':{'x':50,'y':50},'radius':10,'attrs':{'fill':'black'},'euid':5},{'object':'joint','euid':6,'opt':{'vertices':[],'attrs':{'stroke':'#000','fill-opacity':0,'stroke-width':1,'stroke-dasharray':'none','stroke-linecap':'round','stroke-linejoin':'round','stroke-miterlimit':1,'stroke-opacity':1},'cursor':'move','beSmooth':false,'interactive':true,'labelAttrsDefault':{'position':0.5,'offset':0,'font-size':12,'fill':'#000'},'labelAttrs':[],'labelBoxAttrsDefault':{'stroke':'white','fill':'white'},'labelBoxAttrs':[],'bboxCorrection':{'start':{'type':null,'x':0,'y':0,'width':0,'height':0},'end':{'type':null,'x':0,'y':0,'width':0,'height':0}},'dummy':{'start':{'radius':1,'attrs':{'opacity':0,'fill':'red'}},'end':{'radius':1,'attrs':{'opacity':0,'fill':'yellow'}}},'handle':{'timeout':2000,'start':{'enabled':false,'radius':4,'attrs':{'opacity':1,'fill':'red','stroke':'black'}},'end':{'enabled':false,'radius':4,'attrs':{'opacity':1,'fill':'red','stroke':'black'}}},'arrow':{'start':{'path':['M','2','0','L','-2','0'],'dx':2,'dy':2,'attrs':{'opacity':0}},'end':{'path':['M','5','0','L','-5','-5','L','-5','5','z'],'dx':5,'dy':5,'attrs':{'stroke':'black','fill':'black'}}}},'from':2,'to':4,'registered':{'start':[],'end':[],'both':[4,2,3,5]}},{'object':'joint','euid':7,'opt':{'vertices':[],'attrs':{'stroke':'#000','fill-opacity':0,'stroke-width':1,'stroke-dasharray':'none','stroke-linecap':'round','stroke-linejoin':'round','stroke-miterlimit':1,'stroke-opacity':1},'cursor':'move','beSmooth':false,'interactive':true,'labelAttrsDefault':{'position':0.5,'offset':0,'font-size':12,'fill':'#000'},'labelAttrs':[],'labelBoxAttrsDefault':{'stroke':'white','fill':'white'},'labelBoxAttrs':[],'bboxCorrection':{'start':{'type':null,'x':0,'y':0,'width':0,'height':0},'end':{'type':null,'x':0,'y':0,'width':0,'height':0}},'dummy':{'start':{'radius':1,'attrs':{'opacity':0,'fill':'red'}},'end':{'radius':1,'attrs':{'opacity':0,'fill':'yellow'}}},'handle':{'timeout':2000,'start':{'enabled':false,'radius':4,'attrs':{'opacity':1,'fill':'red','stroke':'black'}},'end':{'enabled':false,'radius':4,'attrs':{'opacity':1,'fill':'red','stroke':'black'}}},'arrow':{'start':{'path':['M','2','0','L','-2','0'],'dx':2,'dy':2,'attrs':{'opacity':0}},'end':{'path':['M','5','0','L','-5','-5','L','-5','5','z'],'dx':5,'dy':5,'attrs':{'stroke':'black','fill':'black'}}}},'from':3,'to':2,'registered':{'start':[],'end':[],'both':[4,2,3,5]}},{'object':'joint','euid':8,'opt':{'vertices':[],'attrs':{'stroke':'#000','fill-opacity':0,'stroke-width':1,'stroke-dasharray':'none','stroke-linecap':'round','stroke-linejoin':'round','stroke-miterlimit':1,'stroke-opacity':1},'cursor':'move','beSmooth':false,'interactive':true,'labelAttrsDefault':{'position':0.5,'offset':0,'font-size':12,'fill':'#000'},'labelAttrs':[],'labelBoxAttrsDefault':{'stroke':'white','fill':'white'},'labelBoxAttrs':[],'bboxCorrection':{'start':{'type':null,'x':0,'y':0,'width':0,'height':0},'end':{'type':null,'x':0,'y':0,'width':0,'height':0}},'dummy':{'start':{'radius':1,'attrs':{'opacity':0,'fill':'red'}},'end':{'radius':1,'attrs':{'opacity':0,'fill':'yellow'}}},'handle':{'timeout':2000,'start':{'enabled':false,'radius':4,'attrs':{'opacity':1,'fill':'red','stroke':'black'}},'end':{'enabled':false,'radius':4,'attrs':{'opacity':1,'fill':'red','stroke':'black'}}},'arrow':{'start':{'path':['M','2','0','L','-2','0'],'dx':2,'dy':2,'attrs':{'opacity':0}},'end':{'path':['M','5','0','L','-5','-5','L','-5','5','z'],'dx':5,'dy':5,'attrs':{'stroke':'black','fill':'black'}}}},'from':5,'to':3,'registered':{'start':[],'end':[],'both':[4,2,3,5]}}]";
	}
}

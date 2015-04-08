ИНСТРУКЦИЯ ПО РАБОТЕ С МИГРАЦИЯМИ

1. Если необходимо заполнить базу тестовыми данными, то нужно удалить существующую БД - ILS_3Ducation
   (например, через MS SQL Management Studio), после чего будет создана новая БД с тестовым наполнением.

2. Наполнение тестовыми данными сейчас вынесено в класс ILS.Domain.ILSDBInitializer
   Именно его и только его нужно править. Он не удаляется при создании миграций бд, и его не нужно где-либо
   привязывать, он уже настроен в App.config и Web.config в качестве инициализатора контекста ILS.Domain.ILSContext

3. Порядок действий при работе с миграциями:
   1) Необходимо включить поддержку миграций.
      Для этого необходимо открыть Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console)
	  выбрать в данном окне сверху в качестве проекта (Default project) проект "ILS.Domain" и выполнить комманду:
	     enable-migrations -contexttypename ILSContext
	  Если выдается красным ошибка "Migrations have already been enabled in project", игнорируем её, 
	  это значит что поддержка миграций уже вклчючена
   2) Вносим изменения в структуру классов по стандарту Code First. После этого необходимо создать миграцию.
         Update-Database -StartupProjectName "ILS.Domain" -Verbose -ConnectionString "Server=localhost;Initial Catalog=ILS_3Ducation;Integrated Security=True;MultipleActiveResultSets=True;" -ConnectionProviderName "System.Data.SqlClient"


		 internal sealed class Configuration : DbMigrationsConfiguration<ILS.Domain.ILSContext>
		 public sealed class Configuration : DbMigrationsConfiguration<ILS.Domain.ILSContext>


		 1. Удаляем БД
		 2. Enable-Migrations -Force
		 3. Добавиль код в Configuration
		 4. Add-Migration -Name Initial
		 5. Update-Database
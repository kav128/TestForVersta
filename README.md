# Тестовое задание на позицию .NET разработчика.

Стек используемых технологий:

- ASP.NET Core MVC
- Entity Framework Core
- .NET 6
- Microsoft SQL Server 2022
- Docker

## Инструкция для запуска

Для запуска приложения на компьютере необходим Docker Desktop, а также Docker Compose.
1. Выкачиваем репозиторий командой `git clone https://github.com/kav128/TestForVersta`, переходим в созданную папку `TestForVersta`.
2. Создаем файл `.ENV`, в котором указываем пароль для MS SQL Server, вида `MSSQL_PASSWORD=<your password>`, например: `MSSQL_PASSWORD=ASP.NET_is_the_best_framework`.
Обратите внимание, что SQL Server предъявляет следующие требования к паролю:
       
       The password must be at least 8 characters long and contain characters from three of the following four sets: Uppercase letters, Lowercase letters, Base 10 digits, and Symbols.
3. Запускаем приложение командой `docker-compose up --build -d`.
4. После запуска приложение будет доступно по адресу: [http://localhost](http://localhost).
5. По завершении рабоы с приложением его можно будет остановть командой `docker-compose down`. При этом контейнеры, в которых выполнялось приложение, безвозвратно удаляются вместе со всеми данными.

## Краткий обзор приложения

Домашняя страница одной строкой описывает функционал, а также содержит ссылки на форму добавления заказа и страницу, выводящую все созданные заказы.
Данные ссылки также продублированы на верхней навигационной панели.

При добавлении заказа происходит валидация введенных данных. Добавленные заказы выводятся в таблице без паджинации. 

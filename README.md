# Инструкция по запуску WebAPI приложения для службы доставки

## Описание проекта
Данное WebAPI приложение разработано для фильтрации заказов службы доставки на веб-сервере в зависимости от количества обращений в конкретном районе города и времени, когда был сделан заказ. Оно позволяет обрабатывать заказы, сохраненные в базе данных, и предоставлять результаты фильтрации обратно клиентам.

## Установка
1. **Извлечение архива**
   – Пожалуйста, извлеките содержимое архива "TestWork.rar" в любое удобное для вас место.

2. **Создание базы данных**
   – Для работы вам потребуется СУБД PostgreSQL
   – Создайте новую базу данных (БД) и выполните SQL-запрос для создания таблиц "CityDistricts" и "Orders". Используйте предоставленный SQL-скрипт без изменений.

3. **Импорт данных**
   – Импортируйте данные из файла "CityDistricts.csv" в таблицу "CityDistricts".
   – Аналогично импортируйте данные из файла "Orders.csv" в таблицу "Orders".

## Запуск приложения
1. Откройте проект "TestWork", который находится по следующему пути: "~\TestWork\TestWork\TestWork\TestWork.csproj".
2. Убедитесь, что у вас установлен.NET SDK версии 6.0 или выше.
3. Запустите приложение, открыв программу как HTTP или HTTPS, либо с помощью команды в командной строке: dotnet run.

## Логирование
Все логи хранятся в папке Logs по пути "~\TestWork\TestWork\TestWork\Logs".

## Использование API
В программе используется Swagger UI. После запуска приложения вы можете получить доступ к Swagger UI, который предлагает удобный интерфейс для тестирования API.

## Эндпоинт для фильтрации заказов
URL: POST /api/Filter
Тело запроса (JSON):
{
  "сityDistrictName": "string",
  "startDeliveryOrderDateTime": "yyyy-MM-ddTHH:mm:ssZ"
}
Описание параметров:
- сityDistrictName: Название района доставки.
- startDeliveryOrderDateTime: Время начала доставки в формате yyyy-MM-ddTHH:mm:ssZ.
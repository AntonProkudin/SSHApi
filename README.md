# DigitalEggVirtualManager

DigitalEggVirtualManager позволяет:

- Управлять QEMU-KVM виртуальными машинами на удаленном сервере
- Управлять LXC-контейнерами на удаленном сервере
- Управлять Docker-контейнерами на удаленном сервере

# Для настройки Api необходимо
- в development.json указать путь к базе данных
- в диспетчере менеджера пакетов ввести команды: add-migration init и update-database
- в экземпляре базы данных определить пользователя для авторизациии
- в контроллере произвести первичную настройку для ssh соединения
# Для настройки Client необходимо
- в utils/axios указать адрес api
- https://github.com/AntonProkudin/SSHApi

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=merchant@123" -p 1433:1433 --name Merchant -d mcr.microsoft.com/mssql/server:2019-latest

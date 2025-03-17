# 🏢 FINANZAUTO  

Este proyecto es una API RESTful en **.NET 8** para la gestión de estidaintes, profesores, cursos y notas.  
Permite realizar operaciones **CRUD** (*Crear, Leer, Actualizar y Eliminar*) sobre estos.  

---

## 📌 Requisitos previos  
Antes de clonar el repositorio, asegúrate de tener instalados los siguientes programas:  

📌 [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
🛢️ [SQL Server 2014 o superior](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)  
🖥️ [Visual Studio 2022](https://visualstudio.microsoft.com/) con la carga de trabajo de **desarrollo en .NET**  
🔗 [Git]([https://git-scm.com/](https://github.com/bachiller29/Schoo.git))  
💽 [Postman](https://www.postman.com/) 

---

## 🛄 Base de Datos  

El archivo `ScriptShoolDb.sql` contiene el script necesario para crear la base de datos y las tablas requeridas.  

🔹 **Pasos para ejecutar el script:**  

1️⃣ Abre **SQL Server Management Studio (SSMS)**.  

2️⃣ Conéctate a tu servidor de **SQL Server**.  

3️⃣ Abre el archivo `ScriptShoolDb.sql`.  

4️⃣ **Ejecuta** el script para crear la base de datos y sus tablas.  

💡 **Importante**: Asegúrate de que la base de datos se ha creado correctamente antes de configurar la API.  

---

## ⚙️ Configuración del entorno  

1️⃣ **Clona el repositorio**:  

```sh
git clone https://github.com/bachiller29/Schoo.git
```

2️⃣ **Configura la conexión a la base de datos** en el archivo `appsettings.json`, dentro del proyecto `FinanzautoShool.WebApi`.  

🔹 Busca la sección `"ConnectionStrings"` y edita el valor de la cadena de conexión:  

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=NOMBRE_DB;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;"
}
```

---

## 🛠️ Pruebas con Postman  

Para facilitar las pruebas de la API, se incluye una colección de Postman dentro del proyecto.  

📂 **Ubicación**: `Schoo/Finanzautos.Shool.postman_collection`  

🔹 **Pasos para importar la colección en Postman:**  

1️⃣ Abre **Postman**.  
2️⃣ Ve a la pestaña **"Collections"** en el menú lateral.  
3️⃣ Haz clic en **"Import"**.  
4️⃣ Selecciona el archivo `Finanzautos.Shool.postman_collectio` desde la carpeta `PostmanCollections`.  
5️⃣ Una vez importada, podrás probar los distintos endpoints de la API con las solicitudes preconfiguradas.  

💡 **Nota:** Asegúrate de que la API esté ejecutándose antes de realizar pruebas.  


---

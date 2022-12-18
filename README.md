
<h1 align="center">Password-Manager</h1>

Boveda es un simple, practico y ligero administrador de contraseñas, con el podras almacenar y organizar tus contraseñas, esto te permite utilizar claves mas extensas para mejorar la seguridad de tus cuentas.

<p align="center"><img src="https://user-images.githubusercontent.com/102642378/208306024-d5d63322-079d-4f8a-a554-f74764edaed0.png"/></p> 

## Tabla de contenidos:
---

- [Compilar y ejecutar la app](#compilar-y-ejecutar-la-app)
- [Guia de usuario](#guia-de-usuario)
- [Creacion de usuario](#creacion-de-usuario)
- [Inicio de sesión](#inicio-de-sesión)
- [Añadir claves](#añadir-claves)
- [Borrar claves](#borrar-claves)
- [Copiar al porta papeles](#copiar-al-porta-papeles)


## Compilar y ejecutar la app
---
<ul>
<li>Abrir el proyecto en visualStudio 2022</li>
<li>
Cambie la clave de cifrado interna de la applicación, ubicada en la clase Usuario.cs

        private string masterKEY = "escribe tu clave aca!";

</li>
<li>Seleccione Release y ejecute</li>
<li>El ejecutable con sus dependencias aparecerá en <strong>bin\Release\net6.0-windows\</strong></li>
<li>Si desea usar los iconos que trae la aplicacion puede copiar la carpeta "icons" y su contenido <strong>(bin\Debug\net6.0-windows\)</strong> a la raiz de la carpeta contenedora del ejecutable de la aplicación</li>
</ul>


## Guia de usuario

## Creacion de usuario

para esto es necesario indicar una contraseña maestra, esta será la clave necesaria para cada vez que desee acceder a su "boveda" de contraseñas, adicional a esto se requiere que indique el lugar en el cual desea guardar el archivo donde desea guardar su informacion, aca posee total libertad en la ubicacion y el nombre del archivo que desea crear (sea ingenioso y añada otra capa de seguridad si así lo desea).<br><br>

<p align="center"><img src="https://user-images.githubusercontent.com/102642378/208307049-2306ab03-6898-4e41-b75c-932d97a9c2fb.png"/></p><br><br>
<p align="center"><img src="https://user-images.githubusercontent.com/102642378/208307278-6fa5ab4d-dd6d-47dd-b368-274354d45889.png"/></p><br><br>

## Inicio de sesión

en la pantalla principal del programa, este te indica el lugar del ultimo archivo al que se tuvo acceso (si acabas de creear tu usuario, alli saldrá la direccion de su archivo), tambien le pide ingresar la contraseña maestra para tener acceso al contenido del archivo.<br><br>

<p align="center"><img src="https://user-images.githubusercontent.com/102642378/208307532-784fc7cc-e270-48eb-838c-925fb0bfc794.png"/></p><br><br>

Luego de proporcionar la informacion necesaria, puede hacer click en el boton "Acceder" y de todo estar correcto accederá a la ventana principal de la aplicacion.<br><br>

<p align="center"><img src="https://user-images.githubusercontent.com/102642378/208307659-554d151d-2838-4551-bb6c-88d968188165.png"/></p><br><br>

## Añadir claves

Boveda, cuenta con varios metodos de seguridad uno de estos es el inicio de sesion con tiempo limitado, pues la aplicacion al cabo de un minuto, cerrará la sesion, se puede observar un gran contador regresivo que indica el tiempo restante de la sesion, puede ser molesto, asi que al hacer click en él, el contador se reiniciará.<br><br>

Al hacer click en el boton "Nuevo" se desplegará un menu desde el cual se puede crear un nuevo registro de contraseña, boveda proporciona algunas herramientas con las cuales facilitar este proceso.<br><br>

<p align="center"><img src="https://user-images.githubusercontent.com/102642378/208307995-fff74b1b-3054-45f6-b958-d6957a7d7b0a.png"/></p><br><br>

Boveda te permite elegir un servicio y este colocará un icono para ayudar a la identificacion del elemento, no obstante, el programa no está limitado a los servicios que trae por defecto, usted manualmente puede escribir el nombre de su servicio y el icono asignado será generico, si desea añadir un icono a su servicio, puede colocar su icono en formato <strong>PNG</strong> y con el mismo nombre del servicio al que desea que se añada el icono y respetando las mayusculas, dentro de la carpeta "icons" en la raiz de la aplicacion, ejemplo: <strong>GitHub.png</strong><br><br>

Boveda tambien cuenta con un generador de contraseñas, funciona de forma aleatoria y le permite crear contraseñas desde 8 hasta 64 caracteres, puede deslizar el "TrakBar" para cambiar este valor (por defecto 16), el generador tambien cuenta con una serie de "checkbox" que permiten escoger que caracteres añadir o no a la clave generada (por defecto: todos), haciendo click en el boton "Generar clave(n)", se escribira la clave generada en el campo "clave" de forma visible para usted, y se el permitirá modificarla si asi lo desea.<br><br>

Por ultimo, añada un usuario o informacion necesaria para identificar su item, la imformacion que añada en este campo, será visible en la aplicacion y podra copiarla con un click, habiendo hecho esto, podrá guardar su item.<br><br>

<p align="center"><img src="https://user-images.githubusercontent.com/102642378/208308722-d5842e4b-509f-4f91-ae31-8c99a30633e1.png"/></p><br><br>

## Borrar claves

El proceso de borrado es bastante sencillo, encima del boton "nuevo", está ubicado el boton "eliminar" este boton es el encargado de borrar los items que desee, este se activara solo cuando un item de la lista tenga marcada su casilla de seleccion ubicada a la derecha del item y por defecto desmarcada. Una vez marcada una o varias casillas de items, el boton se activará y podra ser usado, al hacer click en él saldran ventanas de confirmacion para verificar que no fue activado por accidente.<br><br>

## Copiar al porta papeles

Para copiar una clave de un item, basta con hacer click en cualquier parte de este exeptuando la casilla de seleccion y el campo de usuario.<br><br>

Al hacer click en el campo de usuario este se copiará al portapapeles.<br><br>

En la parte superior de la aplicacion se encuentra una barra que indica el estado del portapales, respectivamente para la descripcion o usuario, la clave y el estado vacio.<br><br>

Al copiar cualquier dato al portapapeles, boveda da un tiempo por defecto de 10 segundos para borrar el contenido copiado, este se muestra en una cuenta regresiva y una "ProgresBar", tambien cuenta con un "TrackBar" que permite ajustar este tiempo.







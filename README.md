# StoreHouse 
v1

## Asunciones

### 1. Autenticacion
El mecanismo de autenticación no está implementado. Hay varias implementaciones posibles, pero quizá me decantaría por meter un IdentityServer4. Usariamos JwtBearer junto con “scopes” para asegurarnos que solo los de ese scope pueden acceder y después sólo es controlar qué métodos requieren autenticación con [Authorize] en la cabecera del controller. No he querido meterlo por no alargarme más.

### 2. Autorización
El mecanismo de autorización tampoco está implementado. Usaríamos los claims y los propios roles de dominio para dar distintos niveles de autorización. A mí me gusta usar AD y dejar la gestión de roles y usuarios en el AD. De esa forma evitas problemas de seguridad y tener que hacer la gestión y configuración de los usuarios en tu lado. Sólo hay que definir la estructura de roles y permisos bien (por ejemplo Storehouse_StockUpdater). Y despues en las propias cabeceras de Authorize definir los roles que pueden acceder.

### 3. Filtros
Hay metidos tres mecanismos de filtros en la aplicación
•	Excepcion => se controla a nivel general la excepción. Esto se podría elaborar mucho más y controlar los tipos de excepciones etc… Pero para esta prueba he montado el mecanismo y gestionado de una forma genérica la excepción
•	Validación del modelo => antes de entrar en cualquier método se valida el modelo (a través de fluent validation), si está mal, pues se da un badrequest con los errores encontrados
•	Logging => un filtro para capturar las llamadas antes y después del método y meter algo más de detalle a los logs.

### 4. Base de datos
He metido EF pero con la extensión de FileContextCore (Tb valoré la opción de InMemory), pero esta permite persistir los datos. Para producción evidentemente sería cambiar a SQL o el proveedor que sea y hacer la activación de las migraciones con los comandos de dotnet ef. Seguramente hay que marcar el –startup-project y el –project por la estructura que tengo de proyectos. El fichero lo tenéis en una carpeta llamada “StorehouseDB”

### 5. Logging
Está metido serilog, ahora mismo solo escribe en la consola, pero basta con cambiar el fichero de configuración para que lo haga en otro lado.
No he metido el logging en otras capas porque lo suyo sería hacer algo via AOP para inyectarlo de forma dinámica y no tener que meterlo en cada constructor, existen librerías que ya te lo dan (algunas de pago). En cualquier caso con añadir la dependencia y meter los logs es sufciente.
 
### 6. HTTPS
No esta habilitado pero sería tan sencillo como configurarle el certificado y asignarle el mapping de puertos de docker hacia fuera. 
 
## Arquitectura
He usado un diseño basado en DDD y definido una estructura de carpetas y proyectos que siempre me ha parecido cómoda. He asumido que la solución solicitada tendrá más iteraciones y se requerirá ir añadiendo valor a cada iteración. Por eso puede parecer una solución muy elaborada para los requisitos actuales, pero la idea es dejarla abierta al cambio y la mejora
Aunque se generan un numero grande de proyectos, la estructura en general es muy fácil de leer y entender, pero sobre todo es altamente extensible, evitando líos a posteriori. La idea es que la arquitectura pueda crecer en todas las direcciones y sea altamente extensible.
Las carpetas están ordenadas en la misma forma que una arquitectura de capas: presentación -> aplicación -> dominio -> acceso a datos. En algunas no hay nada como por ejemplo presentación. Además están los proyectos de crosscutting que serían las librerías de apoyo a nivel de todas las capas (como por ejemplo autenticación, autorización…) y dedicadas a cada capa.

## Test
No he implementado todos los test, básicamente porque entiendo que con los test del proyecto “Storehouse.Application.Api.Test” es suficiente para ver el desempeño y no es necesario desarrollar todos los proyectos de test para esta prueba. Aunque en el caso de la capa de dominio he dejado algunos escritos de lo que deberían hacer. Pero estan sin implementar.

## Message broker
No he integrado ninguno en concreto, simplemente he abstraído la implementación del bróker que se use (RabbitMq, Azure ServiceBus…) Se podría hacer una implementación  por cada sistema de colas, dependiendo de si usamos varios. También se podría hacer una implementación especifica por objeto para gestionar los posibles topics o colas que se requieran y que sea todo transparente a través de la interfaz IMessageBroker. 

## Despliegue
Se ejecuta a través de docker, bien via la aplicación de escritorio o por comandos. Por comandos sería ir a una consola de powershell, ubicarte en el mismo directorio de la solución (donde esté el fichero StorehouseApi.sln) y ejecutar los siguientes comandos:
#### Para compilar 
```
docker build --no-cache -t storehouseapplicationapi:dev .
```
#### Para ejecutar
```
docker run -d=false -p 8080:80 --name storehouse.application.api storehouseapplicationapi:dev
```

Después solo es ir en cualquier explorador a la url http://localhost:8080/swagger/index.html


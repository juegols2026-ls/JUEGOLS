# Mapa UTN · Planta baja

Proyecto independiente para **Unity 6.3 LTS, 6000.3.28f1**, con URP 17.3.0.

## Abrir una copia desde GitHub

1. Clonar el repositorio o descargar **Code > Download ZIP** y extraerlo.
2. Instalar **Unity 6.3 LTS 6000.3.28f1** desde Unity Hub.
3. En Unity Hub, elegir **Add project from disk** y seleccionar la carpeta que contiene **Assets**, **Packages** y **ProjectSettings**.
4. Esperar la importación inicial. Unity descargará los paquetes fijados en Packages; esta primera apertura necesita Internet.
5. Abrir **Assets/Scenes/PlantaBaja.unity** y pulsar **Play**. La escena ya está generada: no hace falta ejecutar el generador.

Se incluyen todos los archivos .meta, las mallas, prefabs, materiales, modelos fuente, licencias y los datos locales del mapa. No se necesita adquirir ni importar paquetes de assets por separado.

Library, Temp, Logs y UserSettings son archivos locales que Unity recrea. Las compilaciones de Builds tampoco se versionan: para obtener un ejecutable, usar **UTN > Compilar** e instalar el módulo correspondiente de Unity Hub. Las instrucciones de ejecución de abajo se aplican después de compilar.

## Abrir en Unity

1. En Unity Hub: **Add / Add project from disk**, seleccionar esta carpeta `mapa utn`.
2. Abrir con 6000.3.28f1.
3. Abrir `Assets/Scenes/PlantaBaja.unity` y pulsar Play.

La escena está guardada y se puede editar. La primera importación en otro equipo puede tardar varios minutos.

## Usar sin Unity

- Mac: `Builds/macOS/Mapa UTN.app`.
- Windows: ejecutar `Builds/Windows/Mapa UTN.exe`; conservar la carpeta completa junto al ejecutable.
- Linux x64: ejecutar `Builds/Linux/Mapa UTN.x86_64`; conservar la carpeta completa.
- Web: en este Mac, doble clic en `ABRIR WEB.command` y abrir http://127.0.0.1:8080. Mantener la terminal abierta. No abrir index.html como archivo local.
- En otro equipo con Node.js: `node Herramientas/servir-web.cjs` desde la carpeta del proyecto. Para publicar, servir el contenido de `Builds/Web` por HTTP(S); MIME de `.wasm`: `application/wasm`.

## Controles

**Mapa:** rueda para zoom; botón derecho y arrastre para girar; botón central o WASD/flechas para desplazar; Inicio/Home para centrar. Clic sobre una sala para identificarla. `62 espacios` abre el buscador por nombre o código y centra la sala elegida.

**Recorrido:** botón `Recorrer campus`, WASD/flechas para caminar, mouse para mirar (en Web, mantener el botón derecho mientras se mueve el mouse). Escape libera el cursor; clic en el mundo lo captura nuevamente. `M` o `Vista general` vuelve al mapa. El recorrido comienza en Entrada Rivadavia.

## Contenido

62 espacios de la planta baja, dos patios, circulaciones y cambios de altura basados en Mapear. Mobiliario representativo reducido. Árboles y bancos de la fuente, seis autos estáticos. No hay pisos superiores recorribles, ascensores operativos, personas ni tráfico. Las escaleras a otros pisos están señalizadas y limitadas.

## Regenerar o compilar

- `UTN > Generar planta baja` reconstruye la escena a partir de `Assets/Data`. **Reemplaza `Assets/Generated` y la escena generada**: guardar una copia de la escena antes de personalizarla manualmente.
- `UTN > Validar escena` comprueba los 62 espacios y materiales.
- `UTN > Preparar perfiles y auditar accesos` prepara los cuatro perfiles y verifica interferencias a la altura del cuerpo en las puertas.
- `UTN > Capturar vistas` genera cuatro PNG en `Capturas`.
- `UTN > Compilar` permite generar cada plataforma. Los perfiles están en `Assets/BuildProfiles`.
- `Herramientas/compilar-todo.command` compila las cuatro plataformas en este Mac con Unity cerrado para este proyecto.

Las rutas y nombres de archivo de datos son relativos al proyecto. La aplicación no consulta Mapear ni servicios externos durante la ejecución.

## Revisión y fuentes

Ver `Documentacion/APROXIMACIONES.md` para detalles no verificados del edificio, `Documentacion/FUENTES-Y-LICENCIAS.md` para fuentes y licencias, y `Documentacion/ESTADO-ENTREGA.md` para resultados de pruebas y limitaciones. Las capturas están en `Capturas`.

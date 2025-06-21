[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/LAA5TE63)
[![Open in Codespaces](https://classroom.github.com/assets/launch-codespace-2972f46106e565e64193e422d61a12cf1da4916b45550586e14ef0a7c637dd04.svg)](https://classroom.github.com/open-in-codespaces?assignment_repo_id=19643265)
# SESION DE LABORATORIO N° 08: PRUEBAS UTILIZANDO LA DESARROLLO GUIADO POR EL COMPORTAMIENTO

### Nombre: Sergio Alberto Colque Ponce

## OBJETIVOS
  * Comprender el funcionamiento de las pruebas unitarias dentro de una aplicación utilizando el Framework de pruebas NUnit.

## REQUERIMIENTOS
  * Conocimientos: 
    - Conocimientos básicos de Bash (powershell).
    - Conocimientos básicos de Contenedores (Docker).
  * Hardware:
    - Virtualization activada en el BIOS..
    - CPU SLAT-capable feature.
    - Al menos 4GB de RAM.
  * Software:
    - Windows 10 64bit: Pro, Enterprise o Education (1607 Anniversary Update, Build 14393 o Superior)
    - Docker Desktop 
    - Powershell versión 7.x
    - Net 6
    - Visual Studio Code

## CONSIDERACIONES INICIALES
  * Clonar el repositorio mediante git para tener los recursos necesarios

## DESARROLLO
1. Iniciar la aplicación Powershell o Windows Terminal en modo administrador 
2. Ejecutar el siguiente comando para crear una nueva solución
```
dotnet new sln -o Calculator
```
3. Acceder a la solución creada y ejecutar el siguiente comando para crear una nueva libreria de clases y adicionarla a la solución actual.
```
cd Calculator
dotnet new classlib -o Calculator.Domain
dotnet sln add ./Calculator.Domain/Calculator.Domain.csproj
```
4. Ejecutar el siguiente comando para crear un nuevo proyecto de pruebas y adicionarla a la solución actual
```
dotnet new nunit -o Calculator.Domain.Tests
dotnet sln add ./Calculator.Domain.Tests/Calculator.Domain.Tests.csproj
dotnet add ./Calculator.Domain.Tests/Calculator.Domain.Tests.csproj package SpecFlow.NUnit
dotnet add ./Calculator.Domain.Tests/Calculator.Domain.Tests.csproj package SpecFlow.Plus.LivingDocPlugin
dotnet add ./Calculator.Domain.Tests/Calculator.Domain.Tests.csproj reference ./Calculator.Domain/Calculator.Domain.csproj
```
5. Iniciar Visual Studio Code (VS Code) abriendo el folder de la solución como proyecto. En el proyecto Calculator.Domain, si existe un archivo Class1.cs proceder a eliminarlo. Asimismo en el proyecto Calculator.Domain.Tests si existiese un archivo UnitTest1.cs, también proceder a eliminarlo.

6. En VS Code, en el proyecto Calculator.Domain.Tests proceder a crear el folder "Features" y dentro de este crear el archivo Calculator.feature e introducir la siguiente historia de usuario:
```Gherkin
Feature: Como usuario quiero hacer operaciones aritmeticas para calcular resultados

Scenario: Usuario suma dos numeros y el resultado es correcto
	Given El numero 10
    And el numero 5
	When sumo
	Then el resultado es 15

Scenario: Usuario resta dos numeros y el resultado es correcto
	Given El numero 10
    And el numero 5
	When resto
	Then el resultado es 5
 ```
7. Ahora proceder a crear la clase que brindaa soporte a este caso de usuario, en el proyecto Calculator.Domain, crear el archivo Calculator.cs e introducir el siguiente código.
```C#
namespace Calculator.Domain;
public class Calculator
{
    public int Add(int x, int y) => x + y;
    public int Subtract(int x, int y) => x - y;
}
```
8. Finalmente unir la historia de usuario con la clase mediante pruebas, para esto crear en el proyecto Calculator.Domain.Tests crear el folder "Steps" y dentro de este crear el archivo CalculatorTests.cs e introducir el siguiente código.
```C#
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Calculator.Domain.Tests.Steps
{
    [Binding]
    public sealed class CalculatorTests
    {
        private readonly ScenarioContext _scenarioContext;
        public Calculator Calculadora { get; set; }
        private int _operador01 { get; set; }
        private int _operador02 { get; set; }
        private int _resultado { get; set; }
        
        public CalculatorTests(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            Calculadora = new Calculator();
        }

        [Given("El numero (.*)")]
        public void DadoElNumero(int operando01)
        {
            _operador01 = operando01;
        }

        [Given("el numero (.*)")]
        public void yElNumero(int operando02)
        {
            _operador02 = operando02;
        }

        [When("sumo")]
        public void CuandoSumo()
        {
            _resultado = Calculadora.Add(_operador01,_operador02);
        }

        [When("resto")]
        public void CuandoResto()
        {
            _resultado = Calculadora.Subtract(_operador01,_operador02);
        }

        [Then("el resultado es (.*)")]
        public void EntoncesElResultadoDeberiaSer(int resultado)
        {
            Assert.AreEqual(_resultado, resultado);
        }        
    }
}
```

8. Abrir un terminal en VS Code (CTRL + Ñ) o vuelva al terminal anteriormente abierto, y ejecutar los comandos:
```Bash
dotnet test --collect:"XPlat Code Coverage"
```
9. El resultado debe ser similar al siguiente. 
```Bash
Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     3, Duration: 12 ms
```
10. Finalmente proceder a verificar la cobertura, dentro del proyecto Primes.Tests se dede haber generado una carpeta o directorio TestResults, en el cual posiblemente exista otra subpcarpeta o subdirectorio conteniendo un archivo con nombre `coverage.cobertura.xml`, si existe ese archivo proceder a ejecutar los siguientes comandos desde la linea de comandos abierta anteriomente, de los contrario revisar el paso 8:
```
dotnet tool install -g dotnet-reportgenerator-globaltool
ReportGenerator "-reports:./*/*/*/coverage.cobertura.xml" "-targetdir:Cobertura" -reporttypes:HTML
```
11. El comando anterior primero proceda instalar una herramienta llamada ReportGenerator (https://reportgenerator.io/) la cual mediante la segunda parte del comando permitira generar un reporte en formato HTML con la cobertura obtenida de la ejecución de las pruebas. Este reporte debe localizarse dentro de una carpeta llamada Cobertura y puede acceder a el abriendo con un navegador de internet el archivo index.htm.

12. Finalmente proceder a verificar las pruebas en base a comportamiento, para esto ejecutar el siguiente comando en el terminal anteriormente abierto:
```
dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
livingdoc test-assembly .\Calculator.Domain.Tests\bin\Debug\net7.0\Calculator.Domain.Tests.dll -t .\Calculator.Domain.Tests\bin\Debug\net7.0\TestExecution.json -o CalculatorTests.html
```
13. El comando anterior primero proceda instalar una herramienta llamada Specflow +LivingDoc  (https://specflow.org/tools/living-doc/) la cual mediante la segunda parte del comando permitira generar un reporte en formato HTML con las pruebas en base a comportamiento creadas. Este reporte debe localizarse en el mismo directorio donde se encuentra actualmente y puede acceder a el abriendo con un navegador de internet el archivo index.htm.

---
## Actividades Encargadas
1. Adicionar los escenarios y casos de prueba para multiplicar y dividir.
2. Completar la documentación del Clases, atributos y métodos para luego generar una automatización (publish_docs.yml) que genere la documentación utilizando DocFx y la publique en una Github Page
3. Generar una automatización (publish_bdd_report.yml) que: * Compile el proyecto y ejecute las pruebas, * Genere el reporte BDD, * Publique el reporte en Github Page
4. Generar una automatización (release.yml) que: * Genere el nuget con su codigo de matricula como version del componente, * Publique el nuget en Github Packages, * Genere el release correspondiente

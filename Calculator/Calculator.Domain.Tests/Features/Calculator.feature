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
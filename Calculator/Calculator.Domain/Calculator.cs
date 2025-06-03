namespace Calculator.Domain;

/// <summary>
/// Clase que proporciona operaciones aritméticas básicas.
/// </summary>
public class Calculator
{
    /// <summary>
    /// Suma dos números enteros.
    /// </summary>
    /// <param name="x">Primer operando.</param>
    /// <param name="y">Segundo operando.</param>
    /// <returns>La suma de los dos operandos.</returns>
    public int Add(int x, int y) => x + y;

    /// <summary>
    /// Resta dos números enteros.
    /// </summary>
    /// <param name="x">Primer operando.</param>
    /// <param name="y">Segundo operando.</param>
    /// <returns>La diferencia entre el primer y segundo operando.</returns>
    public int Subtract(int x, int y) => x - y;

    /// <summary>
    /// Multiplica dos números enteros.
    /// </summary>
    /// <param name="x">Primer operando.</param>
    /// <param name="y">Segundo operando.</param>
    /// <returns>El producto de los dos operandos.</returns>
    public int Multiply(int x, int y) => x * y;

    /// <summary>
    /// Divide dos números enteros.
    /// </summary>
    /// <param name="x">Dividendo.</param>
    /// <param name="y">Divisor.</param>
    /// <returns>El cociente de la división.</returns>
    /// <exception cref="System.DivideByZeroException">Se lanza cuando el divisor es cero.</exception>
    public int Divide(int x, int y) => x / y;
}
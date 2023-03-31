namespace SharedResources.Validacoes
{
    public static class ModelValidations
    {
        public static void ThrowForInvalidValue(bool condition, string message)
        {
            if (!condition)
                throw new ArgumentException(message);
        }

        public static void ThrowForEmptyString(string value, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{argumentName} deve ser uma string não vazia");
        }

        public static void ThrowForNotPositiveNumber(int value, string argumentName)
        {
            if (value <= 0)
                throw new ArgumentException($"{argumentName} deve ser um número positivo");
        }

        public static void ThrowForNegativeNumber(int value, string argumentName)
        {
            if (value < 0) throw new ArgumentException($"{argumentName} não pode ser um número negativo");
        }

        public static void ThrowForNullOrEmptyEnumerable<T>(IEnumerable<T> enumerable,string argumentName)
        {
            if (enumerable == null || !enumerable.Any())
                throw new ArgumentException($"{argumentName} não pode ser vazio");

        }
    }
}

namespace GlobalLighting.Utils
{
	/// <summary>
	/// Статический класс генерации случайных чисел
	/// </summary>
	public static class Random
	{
		#region Random members

		/// <summary>
		/// Генератор случайных чисел
		/// </summary>
		private static System.Random R = new System.Random();

		/// <summary>
		/// Генерирует случайное целое неотрицательное число
		/// </summary>
		/// <returns> Целое неотрицательное число </returns>
		public static int Next()
		{
			return R.Next();
		}

		/// <summary>
		/// Генерирует случайное целое неотрицательное число, ограниченное максимумом
		/// </summary>
		/// <param name="maxValue"> Ограничение на генерацию числа </param>
		/// <returns> Целое неотрицательное число </returns>
		public static int Next(int maxValue)
		{
			return R.Next(maxValue);
		}

		/// <summary>
		/// Генерирует случайное целое неотрицательное число в заданном диапазоне
		/// </summary>
		/// <param name="minValue"> Минимальная граница диапазона </param>
		/// <param name="maxValue"> Максимальная граница диапазона </param>
		/// <returns> Целое неотрицательное число </returns>
		public static int Next(int minValue, int maxValue)
		{
			return R.Next(minValue, maxValue);
		}

		/// <summary>
		/// Генерирует случайное рациональное число в диапазоне от 0 до 1
		/// </summary>
		/// <returns> Число из диапазона [0, 1] </returns>
		public static double NextDouble()
		{
			return R.NextDouble();
		}

		#endregion Random members
	}
}
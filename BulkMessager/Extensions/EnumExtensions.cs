namespace BulkMessager.Extensions {
    public static class Enums {
        /// <summary>
        /// Get the value of an enumeration from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">String value</param>
        /// <returns>Enum value of type <typeparamref name="T"/></returns>
        public static T ParseEnum<T>(this string value) where T : Enum 
            => (T)Enum.Parse(typeof(T), value, true);
    }
}

// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("DnjBDpoMqUKztSSYDHwZBjuUUPJ/4urXfCnj/tWQzZkLnHI0JnLjmyvmN82g4s50XAwdZ5Eugn1eVodddU+YtLp4zi6zIJYxjOXRg/xm8NHL7RzMlgre2OJGfIcH8jaaGmlkQzSGBSY0CQINLoJMgvMJBQUFAQQHyqkRt+r8wYB4c5jtZsr7lZpXDGdyLb6KhtsC5sfNoediDeHPEmdR2YYFCwQ0hgUOBoYFBQSn80bvjQPzWMiceuavUSaY+fSlzhQ89NrLYNAxslEmPzR0/ouyXdxOCu5aeqt3ehZkUUsxZCJRgqVtjaI9BLv0BTZUXn+4jD5WJZABmJRKdpZhEeJdhuoCgJXwjKO8+lFFbQ7Odl7DVpERSDfwOk1sPzBRZwYHBQQF");
        private static int[] order = new int[] { 10,6,2,11,7,10,12,7,12,12,11,12,13,13,14 };
        private static int key = 4;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}

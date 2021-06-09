namespace IFEN
{
    public struct Input
    {
        public float In1;
        public float In2;
        public float In3;
        public float In4;
        public float In5;
        public float In6;
        public float In7;
        public float In8;
        public float Inkey;
        public long lu;

        public string Text(string separator)
        {
            return "In1: " + In1 + separator +
                   "In2: " + In2 + separator +
                   "In3: " + In3 + separator +
                   "In4: " + In4 + separator +
                   "In5: " + In5 + separator +
                   "In6: " + In6 + separator +
                   "In7: " + In7 + separator +
                   "In8: " + In8 + separator +
                   "lu: " + lu + separator +
                   "Inkey: " + Inkey;
        }
    }
}
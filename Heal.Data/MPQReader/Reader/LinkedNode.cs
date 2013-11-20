namespace Heal.Data.MpqReader.Reader
{
    internal class LinkedNode
    {
        public LinkedNode Child0;
        public int DecompressedValue;
        public LinkedNode Next;
        public LinkedNode Parent;
        public LinkedNode Prev;
        public int Weight;

        public LinkedNode(int DecompVal, int Weight)
        {
            this.DecompressedValue = DecompVal;
            this.Weight = Weight;
        }

        public LinkedNode Insert(LinkedNode Other)
        {
            if (Other.Weight <= this.Weight)
            {
                if (this.Next != null)
                {
                    this.Next.Prev = Other;
                    Other.Next = this.Next;
                }
                this.Next = Other;
                Other.Prev = this;
                return Other;
            }
            if (this.Prev == null)
            {
                Other.Prev = null;
                this.Prev = Other;
                Other.Next = this;
            }
            else
            {
                this.Prev.Insert(Other);
            }
            return this;
        }

        public LinkedNode Child1
        {
            get
            {
                return this.Child0.Prev;
            }
        }
    }
}



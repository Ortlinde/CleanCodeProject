namespace CleanCodeProject.C10;

public class Cohesion
{
    public class Stack
    {
        private int topOfStack = 0;
        private List<int> elements = new List<int>();

        public int Size()
        {
            return topOfStack;
        }

        public void Push(int element)
        {
            topOfStack++;
            elements.Add(element);
        }

        public int Pop()
        {
            if (topOfStack == 0)
                throw new InvalidOperationException("Stack is empty");

            int element = elements[--topOfStack];
            elements.RemoveAt(topOfStack);
            return element;
        }
    }
}
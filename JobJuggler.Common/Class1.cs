using ZLinq;

namespace JobJuggler.Common;

public class Class1
{
    public void DoSomething()
    {
        var r = Enumerable.Range(1, 10)
            .Select(x => x * x).AsValueEnumerable().Select(x => x * x);
    }
}
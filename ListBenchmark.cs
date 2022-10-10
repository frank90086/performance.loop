using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Performance.List;

[MemoryDiagnoser(false)]
public class ListBenchmark
{
    private readonly Random _random = new(313);
    private List<int> _items;
    
    [Params(100, 10000, 1000000)]
    public int Size { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(1, Size).Select(_ => _random.Next()).ToList();
    }

    [Benchmark]
    public void For()
    {
        for (var i = 0; i < _items.Count; i++) { }
    }
    
    [Benchmark]
    public void Foreach()
    {
        foreach (var item in _items) { }
    }

    [Benchmark]
    public void Foreach_Linq() => _items.ForEach(_ => { });

    [Benchmark]
    public void Parallel_For() => _items.AsParallel().ForAll(_ => { });

    [Benchmark]
    public void Parallel_Foreach() => Parallel.ForEach(_items, _ => { });
    
    [Benchmark]
    public void For_Span()
    {
        for (int i = 0; i < CollectionsMarshal.AsSpan(_items).Length; i++)
        {
            
        }
    }

    [Benchmark]
    public void Foreach_Span()
    {
        foreach (var item in CollectionsMarshal.AsSpan(_items))
        {
            
        }
    }
}

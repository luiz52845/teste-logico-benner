
public class Network
{
    private readonly int[] elementos;
    private readonly int[] tamanhos;

    public Network(int quantidadeElemento)
    {
        if (quantidadeElemento <= 0)
        {
            throw new ArgumentException("A quantidade de elementos deve ser um valor inteiro positivo.");
        }
  
        elementos = new int[quantidadeElemento + 1];
        tamanhos = new int[quantidadeElemento + 1];

        for (int i = 1; i <= quantidadeElemento; i++)
        {
            elementos[i] = i;
            tamanhos[i] = 1;
        }
    }

    public void Conect(int elementoA, int elementoB)
    {
        ValidarElemento(elementoA);
        ValidarElemento(elementoB);

        int raizA = EncontrarElementoRaiz(elementoA);
        int raizB = EncontrarElementoRaiz(elementoB);

        if (raizA != raizB)
        {
            if (tamanhos[raizA] < tamanhos[raizB])
            {
                elementos[raizA] = raizB;
                tamanhos[raizB] += tamanhos[raizA];
            }
            else
            {
                elementos[raizB] = raizA;
                tamanhos[raizA] += tamanhos[raizB];
            }
        }
    }

    public bool query(int elementoA, int elementoB)
    {
        ValidarElemento(elementoA);
        ValidarElemento(elementoB);

        return EncontrarElementoRaiz(elementoA) == EncontrarElementoRaiz(elementoB);
    }

    private int EncontrarElementoRaiz(int elemento)
    {
        while (elemento != elementos[elemento])
        {
            // Compressão de caminho
            elementos[elemento] = elementos[elementos[elemento]];
            elemento = elementos[elemento];
        }
        return elemento;
    }

    private void ValidarElemento(int elemento)
    {
        if (elemento < 1 || elemento >= elementos.Length)
        {
            throw new ArgumentOutOfRangeException("Elemento fora do padrão permitido.");
        }
    }
}

public class Teste
{
    public static void Main(string[] args)
    {
        Network network = new Network(8);
       
        network.Conect(1, 2);
        network.Conect(6, 2);
        network.Conect(2, 4);
        network.Conect(5, 8);

        Console.WriteLine("Consultando conectividade entre 1 e 6: " + network.query(1, 6)); 
        Console.WriteLine("Consultando conectividade entre 6 e 4: " + network.query(6, 4)); 
        Console.WriteLine("Consultando conectividade entre 7 e 4: " + network.query(7, 4)); 
        Console.WriteLine("Consultando conectividade entre 5 e 6: " + network.query(5, 6)); 
        Console.WriteLine("Consultando conectividade entre 1 e 2: " + network.query(1, 2)); 
        Console.WriteLine("Consultando conectividade entre 3 e 7: " + network.query(3, 7)); 
    }
}

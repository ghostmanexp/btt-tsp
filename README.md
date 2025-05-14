# btt-tsp

Este projeto implementa soluções para o Problema do Caixeiro Viajante (TSP - Traveling Salesman Problem) utilizando dois métodos:

1. **Nearest Neighbor TSP**: Uma heurística que encontra uma solução rápida, mas não necessariamente ótima.
2. **TSP Exato**: Algoritmo baseado em programação dinâmica (Held-Karp) que encontra a solução ótima, mas com maior custo computacional.

## Estrutura do Projeto

- `Common/NearestNeighborTSP.cs`: Implementação do algoritmo de heurística do Vizinho Mais Próximo.
- `Common/TSP.cs`: Implementação do algoritmo exato para o TSP.
- `rota.txt`: Arquivo de entrada contendo a matriz de tempos entre os pontos.
- `NearestNeighborTSP.svg` e `TSP.svg`: Arquivos de saída que visualizam as rotas encontradas.

## Como Usar

### Pré-requisitos

- .NET 8.0 ou superior instalado no sistema.

### Passos

1. **Configurar o arquivo de entrada**:
    - Edite o arquivo `rota.txt` para incluir os pontos e a matriz de tempos. O formato deve ser:
      ```text
      A,B,C,D,E
      0,10,15,20,25
      10,0,35,25,30
      15,35,0,30,35
      20,25,30,0,5
      25,30,35,5,0
      ```

2. **Executar o programa**:
    - Compile e execute o projeto no terminal ou na IDE de sua preferência (ex.: JetBrains Rider).
    - O programa calculará as rotas usando ambos os métodos e salvará os resultados.

3. **Visualizar os resultados**:
    - A rota encontrada pelo Vizinho Mais Próximo será salva no arquivo `NearestNeighborTSP.svg`.
    - A rota ótima será salva no arquivo `TSP.svg`.
    - O terminal exibirá as rotas e os tempos totais para comparação.

### Exemplo de Execução

```bash
dotnet run
```

### Saída esperada no terminal:

Método Heurístico (Vizinho Mais Próximo):
Rota encontrada: A -> B -> D -> E -> C -> A
Tempo total: 155

Método Exato (TSP):
Rota ótima: A -> D -> E -> C -> B -> A
Tempo total: 85

Diferença: 82.35% maior que a solução ótima
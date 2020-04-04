using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LabirintoSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string arrivalPath = //local de leitura do arquivo  EX: C:/User/SeuUsuario/Donwload/LabirintoEntrada01.txt;
            string departurePath = //criando arquivo de saida  EX: C:/User/SeuUsuario/Donwload/LabirintoSaida01.txt;

            // Se existe um arquivo com nome igual ao que vai ser gerado vai excluir o existente
            if (File.Exists(departurePath))
            {
                File.Delete(departurePath);
            }
            try
            {
                using (FileStream file = new FileStream(arrivalPath, FileMode.Open))
                using (StreamReader fileReader = new StreamReader(file))
                {
                    // Lista que vai usar como refência para voltas
                    List<string> road = new List<string>();

                    // Lista que vai escrever e mostar o resultado
                    List<string> roadLog = new List<string>();
                    // Detectando a quantidade de linha e colunas
                    string linesAndColumn = fileReader.ReadLine().Replace("\r\n", "");
                    // Lendo todo conteudo que vai formar o labirinto
                    string text = fileReader.ReadToEnd().Replace(" ", "").Replace("\r\n", "");

                    //vertice X e verticeY
                    int posX = 0;
                    int posY = 0;

                    int k = 0;
                    int a = 1;
                    // Fixa o contador de subtração da lista
                    int count = 1;
                    // Dividindo numero de linhas do labirinto e de colunas do labirinto
                    int lineLength = int.Parse(linesAndColumn.Substring(0, linesAndColumn.IndexOf(" ")));
                    int columnLength = int.Parse(linesAndColumn.Substring(linesAndColumn.IndexOf(" ") + 1));
                    String[,] labyrinth = new String[lineLength + 2, columnLength + 2];

                    for (int i = 1; i <= lineLength; i++)
                    {
                        for (int j = 1; j <= columnLength; j++)
                        {
                            labyrinth[i, j] = (text.Substring(k, 1).ToUpper());
                            k++;

                            if (labyrinth[i, j] == "X")
                            {
                                posX = i;
                                posY = j;
                                road.Add($"{posX}, {posY}");
                                roadLog.Add($"O [{posX}, {posY}]");
                            }
                        }
                    }
                    bool inLab = true;
                    while (inLab)
                    {
                        if (IsUpFree(labyrinth, posX, posY) || IsLeftFree(labyrinth, posX, posY) ||
                            IsRightFree(labyrinth, posX, posY) || IsDownFree(labyrinth, posX, posY))
                        {
                            switch (a)
                            {
                                case 1:
                                    if (IsUpFree(labyrinth, posX, posY))
                                    {
                                        DecreaseAPosition(posX);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"C [{posX}, {posY}]");
                                    }
                                    else if (IsLeftFree(labyrinth, posX, posY))
                                    {
                                        DecreaseAPosition(posY);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"E [{posX}, {posY}]");
                                    }
                                    else if (IsRightFree(labyrinth, posX, posY))
                                    {
                                        increaseAPosition(posY);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"D [{posX}, {posY}]");
                                    }
                                    else if (IsDownFree(labyrinth, posX, posY))
                                    {
                                        increaseAPosition(posX);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"B [{posX}, {posY}]");
                                    }
                                    if (labyrinth[posX, posY] == "A")
                                    {
                                        a += 1;
                                    }
                                    labyrinth[posX, posY] = "00";
                                    break;

                                case 2:
                                    if (IsLeftFree(labyrinth, posX, posY))
                                    {
                                        DecreaseAPosition(posY);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"E [{posX}, {posY}]");
                                    }
                                    else if (IsRightFree(labyrinth, posX, posY))
                                    {
                                        increaseAPosition(posY);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"D [{posX}, {posY}]");
                                    }
                                    else if (IsDownFree(labyrinth, posX, posY))
                                    {
                                        increaseAPosition(posX);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"B [{posX}, {posY}]");
                                    }
                                    else if (IsUpFree(labyrinth, posX, posY))
                                    {
                                        DecreaseAPosition(posX);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"C [{posX}, {posY}]");
                                    }
                                    if (labyrinth[posX, posY] == "A")
                                    {
                                        a += 1;
                                    }
                                    labyrinth[posX, posY] = "00";
                                    break;

                                case 3:
                                    if (IsRightFree(labyrinth, posX, posY))
                                    {
                                        increaseAPosition(posY);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"D [{posX}, {posY}]");
                                    }
                                    else if (IsDownFree(labyrinth, posX, posY))
                                    {
                                        increaseAPosition(posX);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"B [{posX}, {posY}]");
                                    }
                                    else if (IsUpFree(labyrinth, posX, posY))
                                    {
                                        DecreaseAPosition(posX);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"C [{posX}, {posY}]");
                                    }
                                    else if (IsLeftFree(labyrinth, posX, posY))
                                    {
                                        DecreaseAPosition(posY);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"E [{posX}, {posY}]");
                                    }
                                    if (labyrinth[posX, posY] == "A")
                                    {
                                        a += 1;
                                    }
                                    labyrinth[posX, posY] = "00";
                                    break;

                                case 4:
                                    if (IsDownFree(labyrinth, posX, posY))
                                    {
                                        increaseAPosition(posX);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"B [{posX}, {posY}]");
                                    }
                                    else if (IsUpFree(labyrinth, posX, posY))
                                    {
                                        DecreaseAPosition(posX);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"C [{posX}, {posY}]");
                                    }
                                    else if (IsLeftFree(labyrinth, posX, posY))
                                    {
                                        DecreaseAPosition(posY);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"E [{posX}, {posY}]");
                                    }
                                    else if (IsRightFree(labyrinth, posX, posY))
                                    {
                                        increaseAPosition(posY);
                                        road.Add($"{posX}, {posY}");
                                        roadLog.Add($"D [{posX}, {posY}]");
                                    }
                                    if (labyrinth[posX, posY] == "A")
                                    {
                                        a = 1;
                                    }
                                    labyrinth[posX, posY] = "00";
                                    break;
                            }
                        }
                        //checando se o a saida está diposnivel
                        else if (labyrinth[posX - 1, posY] == null || labyrinth[posX, posY - 1] == null || labyrinth[posX, posY + 1] == null || labyrinth[posX + 1, posY] == null)
                        {
                            inLab = false;
                        }
                        //Se não há mais rotas livres, voltar pelo caminho que veio
                        else if (road.Count() >= 1)
                        {
                            road.Remove(road[road.Count() - count]);
                            int xOldValue = int.Parse(road[road.Count() - count].Substring(0, road[road.Count() - count].IndexOf(",")));
                            int yOldValue = int.Parse(road[road.Count() - count].Substring(road[road.Count() - count].IndexOf(" ") + 1));

                            if (xOldValue > posX)
                            {
                                posX = xOldValue;
                                roadLog.Add($"B [{posX}, {posY}]");
                            }
                            else if (yOldValue > posY)
                            {
                                posY = yOldValue;
                                roadLog.Add($"D [{posX}, {posY}]");
                            }
                            else if (yOldValue < posY)
                            {
                                posY = yOldValue;
                                roadLog.Add($"E [{posX}, {posY}]");
                            }
                            else if (xOldValue < posX)
                            {
                                posX = xOldValue;
                                roadLog.Add($"C [{posX}, {posY}]");
                            }
                        }
                    }

                    using (StreamWriter sw = File.AppendText(departurePath))
                    {
                        foreach (string log in roadLog)
                        {
                            sw.WriteLine(log);
                        }
                    }
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine("An unexpected error!");
                Console.WriteLine(exception.Message);
            }

            bool IsUpFree(string[,] labyrinth, int posX, int posY)
            {
                if (labyrinth[posX - 1, posY] == "0" || labyrinth[posX - 1, posY] == "A")
                {
                    return true;
                }
                return false;
            }
            bool IsLeftFree(string[,] labyrinth, int posX, int posY)
            {
                if (labyrinth[posX, posY - 1] == "0" || labyrinth[posX, posY - 1] == "A")
                {
                    return true;
                }
                return false;
            }
            bool IsRightFree(string[,] labyrinth, int posX, int posY)
            {
                if (labyrinth[posX, posY + 1] == "0" || labyrinth[posX, posY + 1] == "A")
                {
                    return true;
                }
                return false;
            }
            bool IsDownFree(string[,] labyrinth, int posX, int posY)
            {
                if (labyrinth[posX + 1, posY] == "0" || labyrinth[posX + 1, posY] == "A")
                {
                    return true;
                }
                return false;
            }

            int DecreaseAPosition(int position)
            {
                return position -= 1;
            }

            int increaseAPosition(int position)
            {
                
                return position += 1;
            }

        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;

namespace AdministrarTiendaEnlinea
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Producto producto = new Producto();
            Metodos metodos = new Metodos();
            ProductosListas productoslistas = new ProductosListas();

            List<(string, List<string>)> TodasListas = new List<(string, List<string>)>
            {
                (metodos.TextoVerde("Categorías Tecnológicas"), productoslistas.categoriasTecnologicas),
                (metodos.TextoVerde("Computadoras y Laptops"), productoslistas.ComputadorasLaptops),
                (metodos.TextoVerde("Dispositivos Móviles y Accesorios"), productoslistas.DispositivosMóvilesAccesorios),
                (metodos.TextoVerde("Audio y Entretenimiento"), productoslistas.AudioEntretenimiento)
            };

            int Opcion;
            do
            {
                metodos.MenuPrincipal();
                if (int.TryParse(Console.ReadLine(), out Opcion))
                {
                    switch (Opcion)
                    {
                        case 1:
                            Console.WriteLine("\nMostrando todos los productos disponibles\n");
                            metodos.MostrarListas(TodasListas);
                            break;

                        case 2:
                            int OpcionCategorias = metodos.SubmenuCategorias();
                            switch (OpcionCategorias)
                            {
                                case 1:
                                    metodos.AgregarProductoEnCategoria(productoslistas.ComputadorasLaptops);
                                    metodos.MostrarListas(TodasListas);
                                    break;

                                case 2:
                                    metodos.AgregarProductoEnCategoria(productoslistas.DispositivosMóvilesAccesorios);
                                    metodos.MostrarListas(TodasListas);
                                    break;

                                case 3:
                                    metodos.AgregarProductoEnCategoria(productoslistas.AudioEntretenimiento);
                                    metodos.MostrarListas(TodasListas);
                                    break;

                                case 4:
                                    Console.WriteLine("\nSaliendo del submenu...\n");
                                    break;

                                default:
                                    metodos.TextoRojo("\nOpción no válida. Por favor, ingrese un número válido.\n");
                                    break;
                            }
                            break;

                        case 3:
                            metodos.EditarProducto(TodasListas);
                            break;

                        case 4:
                            metodos.EliminarProductoDeCategoria(TodasListas);
                            break;

                        case 5:
                            metodos.RealizarCompra(productoslistas);
                            break;

                        case 6:
                            metodos.MostrarProductosMasComprados(productoslistas);
                            break;

                        case 7:
                            Console.WriteLine("Salir del programa...");
                            break;

                        default:
                            metodos.TextoRojo("\nOpcion no valida...\n");
                            break;
                    }
                }
                else
                {
                    metodos.TextoRojo("\nOpcion invalida...\n");
                }

            } while (Opcion != 9);
        }
    }

    public class Producto
    {
        public string? Marca { get; set; }
        public string? NombreProducto { get; set; }
        public double PrecioProducto { get; set; }

        // Constructor
        public Producto(string marca, string nombre, double precio)
        {
            Marca = marca;
            NombreProducto = nombre;
            PrecioProducto = precio;
        }

        // Constructor vacío
        public Producto() { }
    }


    public class ProductosListas
    {
        public List<string> categoriasTecnologicas = new List<string>
        {
            "Computadoras y Laptops",
            "Dispositivos Móviles y Accesorios",
            "Audio y Entretenimiento"
        };

        public List<string> ComputadorasLaptops = new List<string>
        {
            "Computadoras de escritorio",
            "Laptops",
            "PCs todo en uno",
            "Laptops/desktops para juegos",
            "Estaciones de trabajo",
            "Chromebooks",
            "Mini PCs",
        };

        public List<string> DispositivosMóvilesAccesorios = new List<string>
        {
            "Teléfonos inteligentes",
            "Tabletas",
            "Smartwatches",
            "Rastreadores de fitness",
            "Fundas para teléfonos",
            "Protectores de pantalla",
            "Cargadores y cables",
            "Bancos de energía",
        };

        public List<string> AudioEntretenimiento = new List<string>
        {
            "Auriculares",
            "Altavoces inalámbricos",
            "Sistemas de cine en casa",
            "Barras de sonido",
            "Reproductores de MP3",
            "Micrófonos",
            "Equipos de DJ"
        };

        public Dictionary<string, int> VentasProductos { get; set; }

        public ProductosListas()
        {
            VentasProductos = new Dictionary<string, int>();
        }
    }

    public class VentaProducto
    {
        public string? NombreProducto { get; set; }
        public int CantidadVendida { get; set; }
    }

    public class Metodos
    {
        public void MenuPrincipal()
        {
            Console.WriteLine("************Bienvenido a tienda WILFREDO************");
            Console.WriteLine("1. Ver productos disponibles");
            Console.WriteLine("2. Agregar productos");
            Console.WriteLine("3. Editar Productos");
            Console.WriteLine("4. Eliminar productos");
            Console.WriteLine("5. Realizar una compra");
            Console.WriteLine("6. Mostrar los 3 productos más vendidos");
            Console.WriteLine("7. Salir");


            Console.Write("\nIngrese una de las opciones del menu: ");
        }

        public int SubmenuCategorias()
        {
            Console.WriteLine("\n*************Submenu de Categorias*************");
            Console.WriteLine("1. Computadoras y Laptops");
            Console.WriteLine("2. Dispositivos Móviles y Accesorios");
            Console.WriteLine("3. Audio y Entretenimiento");
            Console.WriteLine("4. Salir del submenu..");
            Console.Write("\n¿Cuál de las categorías te gustaría editar?: ");

            int opcion;
            if (int.TryParse(Console.ReadLine(), out opcion))
            {
                return opcion;
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
                return 0;
            }
        }

        public void AgregarProductoEnCategoria(List<string> categoria)
        {
            Console.Write("Ingrese la marca del producto: ");
            string? marca = Console.ReadLine();

            Console.Write("Ingrese el nombre del producto: ");
            string? nombre = Console.ReadLine();

            Console.Write("Ingrese el precio del producto: $");
            double precio;
            while (!double.TryParse(Console.ReadLine(), out precio))
            {
                Console.WriteLine(TextoRojo("\nPrecio inválido. Por favor, ingrese un precio válido.\n"));
                Console.Write("Ingrese el precio del producto: $");
            }

            categoria.Add($"Marca: {marca}, Nombre: {nombre}, Precio: ${precio}");

            Console.WriteLine(TextoVerde("\nProducto agregado con éxito.\n"));
        }

        public void EditarProducto(List<(string, List<string>)> ProductosListas)
        {
            Console.WriteLine("Categorías disponibles para editar:");
            for (int i = 0; i < ProductosListas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ProductosListas[i].Item1}");
            }

            Console.Write("\nSeleccione la categoría que desea editar: ");
            int categoriaIndex;
            if (!int.TryParse(Console.ReadLine(), out categoriaIndex) || categoriaIndex < 1 || categoriaIndex > ProductosListas.Count)
            {
                Console.WriteLine("Selección inválida. Por favor, ingrese un número válido.");
                return;
            }

            var categoriaSeleccionada = ProductosListas[categoriaIndex - 1];
            var productosEnCategoria = categoriaSeleccionada.Item2;

            Console.WriteLine($"\nProductos en la categoría '{categoriaSeleccionada.Item1}':");
            for (int i = 0; i < productosEnCategoria.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {productosEnCategoria[i]}");
            }

            Console.Write("\nSeleccione el número del producto que desea editar: ");

            int productoIndex;
            if (!int.TryParse(Console.ReadLine(), out productoIndex) || productoIndex < 1 || productoIndex > productosEnCategoria.Count)
            {
                TextoRojo("\nSelección inválida. Por favor, ingrese un número válido.\n");
                return;
            }

            string? productoSeleccionado = productosEnCategoria[productoIndex - 1];

            Console.WriteLine("\nIngrese los nuevos detalles del producto:");
            Console.Write("Marca: ");
            string? nuevaMarca = Console.ReadLine();
            Console.Write("Nombre: ");
            string? nuevoNombre = Console.ReadLine();
            double nuevoPrecio;
            do
            {
                Console.Write("Precio: $");
            } while (!double.TryParse(Console.ReadLine(), out nuevoPrecio) || nuevoPrecio < 0);

            string? nuevoProducto = $"Marca: {nuevaMarca}, Nombre: {nuevoNombre}, Precio: ${nuevoPrecio}";

            productosEnCategoria[productoIndex - 1] = nuevoProducto;

            Console.WriteLine("\nProducto editado con éxito.\n");
        }

        public void EliminarProductoDeCategoria(List<(string, List<string>)> ProductosListas)
        {
            Console.WriteLine("\nSeleccione la categoría de la cual desea eliminar un producto:");
            for (int i = 0; i < ProductosListas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ProductosListas[i].Item1}");
            }

            int categoriaIndex = -1;
            while (categoriaIndex < 1 || categoriaIndex > ProductosListas.Count)
            {
                Console.Write("\nIngrese el número de la categoría: ");
                if (!int.TryParse(Console.ReadLine(), out categoriaIndex))
                {
                    Console.WriteLine(TextoRojo("\nSelección inválida. Por favor, ingrese un número válido.\n"));
                    categoriaIndex = -1;
                }
            }

            var categoriaSeleccionada = ProductosListas[categoriaIndex - 1];
            var productosEnCategoria = categoriaSeleccionada.Item2;

            Console.WriteLine($"\nProductos en la categoría '{categoriaSeleccionada.Item1}':");
            for (int i = 0; i < productosEnCategoria.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {productosEnCategoria[i]}");
            }

            int productoIndex = -1;
            while (productoIndex < 1 || productoIndex > productosEnCategoria.Count)
            {
                Console.Write("\nSeleccione el número del producto que desea eliminar: ");
                if (!int.TryParse(Console.ReadLine(), out productoIndex))
                {
                    Console.WriteLine(TextoRojo("\nSelección inválida. Por favor, ingrese un número válido.\n"));
                    productoIndex = -1;
                }
            }

            productosEnCategoria.RemoveAt(productoIndex - 1);
            Console.WriteLine(TextoVerde("\nProducto eliminado con éxito.\n"));
        }

        public void MostrarListas(List<(string, List<string>)> ListasTodas)
        {
            foreach (var productos in ListasTodas)
            {
                Console.WriteLine(productos.Item1 + ":");
                List<string> lista = productos.Item2;
                for (int i = 0; i < lista.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {lista[i]}");
                }
                Console.WriteLine();
            }
        }

        public void RealizarCompra(ProductosListas productoslistas)
        {
            Console.WriteLine("************Productos Disponibles para Comprar************");
            int count = 1;
            foreach (var categoria in productoslistas.categoriasTecnologicas)
            {
                Console.WriteLine($"\n{categoria}:");
                var productosEnCategoria = productoslistas.ComputadorasLaptops; // Por defecto
                if (categoria == "Computadoras y Laptops")
                {
                    productosEnCategoria = productoslistas.ComputadorasLaptops;
                }
                else if (categoria == "Dispositivos Móviles y Accesorios")
                {
                    productosEnCategoria = productoslistas.DispositivosMóvilesAccesorios;
                }
                else if (categoria == "Audio y Entretenimiento")
                {
                    productosEnCategoria = productoslistas.AudioEntretenimiento;
                }

                foreach (var producto in productosEnCategoria)
                {
                    Console.WriteLine($"{count++}. {producto}");
                }
            }

            Console.Write("\nSeleccione el número del producto que desea comprar: ");
            int productoIndex;
            if (int.TryParse(Console.ReadLine(), out productoIndex))
            {
                if (productoIndex > 0 && productoIndex <= productoslistas.categoriasTecnologicas.Count)
                {
                    // Obtener el nombre del producto seleccionado
                    string selectedProduct = "";
                    int currentCount = 0;
                    foreach (var categoria in productoslistas.categoriasTecnologicas)
                    {
                        var productosEnCategoria = productoslistas.ComputadorasLaptops; // Por defecto
                        if (categoria == "Computadoras y Laptops")
                        {
                            productosEnCategoria = productoslistas.ComputadorasLaptops;
                        }
                        else if (categoria == "Dispositivos Móviles y Accesorios")
                        {
                            productosEnCategoria = productoslistas.DispositivosMóvilesAccesorios;
                        }
                        else if (categoria == "Audio y Entretenimiento")
                        {
                            productosEnCategoria = productoslistas.AudioEntretenimiento;
                        }

                        currentCount += productosEnCategoria.Count;
                        if (productoIndex <= currentCount)
                        {
                            selectedProduct = productosEnCategoria[productoIndex - (currentCount - productosEnCategoria.Count) - 1];
                            break;
                        }
                    }

                    if (productoslistas.VentasProductos.ContainsKey(selectedProduct))
                    {
                        productoslistas.VentasProductos[selectedProduct]++;
                    }
                    else
                    {
                        productoslistas.VentasProductos[selectedProduct] = 1;
                    }

                    Console.WriteLine($"\nProducto '{selectedProduct}' comprado con éxito.\n");
                }
                else
                {
                    Console.WriteLine("Número de producto inválido.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
            }
        }


        public void MostrarProductosMasComprados(ProductosListas productoslistas)
        {
            var productosOrdenados = productoslistas.VentasProductos.OrderByDescending(pair => pair.Value).Take(3);

            Console.WriteLine("\nLos 3 productos más comprados son:");
            foreach (var producto in productosOrdenados)
            {
                Console.WriteLine($"{producto.Key}, Cantidad Comprada: {producto.Value}");
            }
        }

        public void CalcularDescuentos(ProductosListas productoslistas)
        {
            Console.WriteLine("\nCalculando descuentos...\n");
            // Lógica para calcular descuentos
        }

        public string TextoRojo(string Texto)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Texto);
            Console.ResetColor();

            return Texto;
        }

        public string TextoVerde(string texto)
        {
            return $"\u001b[32m{texto}\u001b[0m";
        }
    }
}

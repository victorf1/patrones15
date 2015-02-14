public abstract class EstadoPedido
{
    protected Pedido pedido;

    public EstadoPedido(Pedido pedido)
    {
        this.pedido = pedido;
    }

    public abstract void agregaProducto(Producto producto);
    public abstract void borra();
    public abstract void suprimeProducto(Producto producto);
    public abstract EstadoPedido estadoSiguiente();
}


// pedido
using System;
using System.Collections.Generic;

public class Pedido
{
    protected IList<Producto> productos = new List<Producto>();
    public IList<Producto> Productos
    {
        get
        {
            return productos;
        }
    }
    protected EstadoPedido estadoPedido;

    public Pedido()
    {
        estadoPedido = new PedidoEnCurso(this);
    }

    public void agregaProducto(Producto producto)
    {
        estadoPedido.agregaProducto(producto);
    }

    public void suprimeProducto(Producto producto)
    {
        estadoPedido.suprimeProducto(producto);
    }

    public void borra()
    {
        estadoPedido.borra();
    }

    public void estadoSiguiente()
    {
        estadoPedido = estadoPedido.estadoSiguiente();
    }

    public void visualiza()
    {
        Console.WriteLine("Contenido del pedido");
        foreach (Producto producto in productos)
        
        producto.visualiza();
        Console.WriteLine();
    }
}




//pedido en curso


public class PedidoEnCurso : EstadoPedido
{
    public PedidoEnCurso(Pedido pedido)
        : base
    (pedido) { }

    public override void agregaProducto(Producto producto)
    {
        pedido.Productos.Add(producto);
    }

    public override void borra()
    {
        pedido.Productos.Clear();
    }

    public override void suprimeProducto(Producto producto)
    {
        pedido.Productos.Remove(producto);
    }

    public override EstadoPedido estadoSiguiente()
    {
        return new PedidoValidado(pedido);
    }
}



//pedido entregado
public class PedidoEntregado : EstadoPedido
{
    public PedidoEntregado(Pedido pedido)
        : base(pedido)
    { }

    public override void agregaProducto(Producto producto) { }

    public override void borra() { }

    public override void suprimeProducto(Producto producto) { }

    public override EstadoPedido estadoSiguiente()
    {
        return this;
    }
}

// estado pedido
public class PedidoValidado : EstadoPedido
{
    public PedidoValidado(Pedido pedido)
        : base
    (pedido) { }

    public override void agregaProducto(Producto producto) { }

    public override void borra()
    {
        pedido.Productos.Clear();
    }

    public override void suprimeProducto(Producto producto) { }

    public override EstadoPedido estadoSiguiente()
    {
        return new PedidoEntregado(pedido);
    }
}


//pedido
using System;

public class Producto
{
    protected string nombre;

    public Producto(string nombre)
    {
        this.nombre = nombre;
    }

    public void visualiza()
    {
        Console.WriteLine("Producto: " + nombre);
    }
}


// usuario

public class Usuario
{
    static void Main(string[] args)
    {
        Pedido pedido = new Pedido();
        pedido.agregaProducto(new Producto("vehículo 1"));
        pedido.agregaProducto(new Producto("accesorio 2"));
        pedido.visualiza();
        pedido.estadoSiguiente();
        pedido.agregaProducto(new Producto("accesorio 3"));
        pedido.borra();
        pedido.visualiza();

        Pedido pedido2 = new Pedido();
        pedido2.agregaProducto(new Producto("vehículo 11"));
        pedido2.agregaProducto(new Producto("accesorio 21"));
        pedido2.visualiza();
        pedido2.estadoSiguiente();
        pedido2.visualiza();
        pedido2.estadoSiguiente();
        pedido2.borra();
        pedido2.visualiza();
    }
}

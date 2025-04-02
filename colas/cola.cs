namespace colas;
public class cola{ // clase para enlazar nodos
    public Nodo primero;
    public int contador;

    public cola(){
        primero = null;
        contador = 0;
    }

    public void agregarAlFinal(object valor1, object valor2){ // Agregar al Final
        Nodo nuevoNodo = new Nodo (valor1, valor2);
        if (primero == null){
            primero = nuevoNodo;
        } else {
            Nodo actual = primero;
            while (actual.Siguiente != null){
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
        contador++;
    }

    public void agregarAlFinalCaja(object valor1, object valor2, object caja){
        Nodo nuevoNodo = new Nodo (valor1, valor2, caja);
        if (primero == null){
            primero = nuevoNodo;
        } else {
            Nodo actual = primero;
            while (actual.Siguiente != null){
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
        contador++;
    }

    public (object,  object) eliminarFrente(){ // Eliminar El Primero de la cola
        if (primero == null) return (null, null);
        Nodo auxiliar = primero;

        if (primero.Siguiente == null){
            primero = null;
            contador--;
            return (auxiliar.Valor1, auxiliar.Valor2);
        } else {
            primero = primero.Siguiente;
            contador--;
            return (auxiliar.Valor1, auxiliar.Valor2);
        }
    }

    public void eliminarPorValor(object valor){ // Elimniar Por Valor
        if (primero == null) return;

        if (primero.caja.Equals(valor)){
            primero = primero.Siguiente;
            contador --;
            return;
        }

        Nodo actual = primero;
        while (actual.Siguiente != null && !actual.Siguiente.caja.Equals(valor)){
            actual = actual.Siguiente;
        }

        if (actual.Siguiente != null){
            actual.Siguiente = actual.Siguiente.Siguiente;
            contador --;
        }
    }

    public void retornarPrimero(int x, int y){
        printxy(x, y+=2, $"Nombre Del Cliente: {primero.Valor1}");

        printxy(x, y+=2, $"Numero Asignado: {primero.Valor2}");
    }


public int enlistar(int x, int y){
Nodo actual = primero;
int disponibles = 0;
    while (actual != null){
            printxy(x, y+=1, $"{actual.Valor2} - {actual.Valor1})");
            disponibles++;
            actual = actual.Siguiente;
        }
    return disponibles;
}

public int cajas(int x, int y){
Nodo actual = primero;
int disponibles = 0;
    while (actual != null){
            printxy(x, y+=1, $"{actual.caja})");
            disponibles++;
            actual = actual.Siguiente;
        }
    return disponibles;
}

public int enlistarCajasCliente(int x, int y){
Nodo actual = primero;
int disponibles = 0;

            printxy(x, y+=1, "Caja");
            printxy(x+16, y, $"Numero Cliente");
            printxy(x+38, y, $"Nombre Cliente");
    while (actual != null){
            printxy(x, y+=1, $"{actual.caja}");
            printxy(x+16, y, $"{actual.Valor2}");
            printxy(x+38, y, $"{actual.Valor1}");
            disponibles++;
            actual = actual.Siguiente;
        }
    return disponibles;
}

public int enlistarDisponibles(int x, int y, cola cola){
Nodo actual = primero;
Nodo actual_cola = cola.primero;
int disponibles = 0;
bool mostrar=true;

    while (actual != null){
        mostrar=true;
        actual_cola = cola.primero;
        while (actual_cola != null){
            if (actual_cola.caja.Equals(actual.Valor2)) mostrar = false;
            actual_cola = actual_cola.Siguiente;
        }
        if (mostrar){
            printxy(x, y+=1, $"{actual.Valor2})");
            disponibles++;
        }
        actual = actual.Siguiente;
    }
    return disponibles;
}


public int enlistarOcupadas(int x, int y, cola cola){
Nodo actual = primero;
Nodo actual_cola;
int disponibles = 0;
bool aumentar=true;

    while (actual != null){
        aumentar=true;
        actual_cola = cola.primero;
        while (actual_cola != null){
            if (actual_cola.caja.Equals(actual.Valor2)) aumentar = false;
            actual_cola = actual_cola.Siguiente;
        }
        if (!aumentar) disponibles++;

        actual = actual.Siguiente;
    }
    return disponibles;
}


public bool cajaOcupada(int numeroCaja) {
    Nodo actual = primero;

    if (numeroCaja >= 4 || numeroCaja <= 0 ) return true;
    while (actual != null) {
        if (actual.caja.Equals(numeroCaja)) {
            return true;
        }
        actual = actual.Siguiente;
    }
    return false;
}

    public static void printxy(int x, int y, string mensaje){
        Console.SetCursorPosition(x,y);
        Console.Write(mensaje);
    }

}
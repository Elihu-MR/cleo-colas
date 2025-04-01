// Fecha 31/3/2025

// Equipo:
// Meza Osuna Juan Manuel
// Moreno Ramirez Josue Elihu
// Perez Salazar Jese Santiago

// Cuatrimestre y Grupo: 4B

namespace colas;

public class AProgram{

public static cola cajas_VIP = new cola(); // Lista de cajas VIP
public static cola cajas_Normales = new cola(); // Lista de cajas VIP
public static cola llegada = new cola();
public static cola cola_VIP = new cola();
public static cola cola_Normal = new cola();
public static cola asignado_caja_Normal = new cola();
public static cola asignado_caja_VIP = new cola();

public static int cliente_contador = 1;

public static void Main(string[] args){
    cajas_VIP.agregarAlFinal("VIP", 1);
    cajas_VIP.agregarAlFinal("VIP", 2);
    cajas_VIP.agregarAlFinal("VIP", 3);

    cajas_Normales.agregarAlFinal("Normal", 1);
    cajas_Normales.agregarAlFinal("Normal", 2);
    cajas_Normales.agregarAlFinal("Normal", 3);

    menu();
} // Main

public static void titulo(string texto){
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write(texto);
    Console.ForegroundColor = ConsoleColor.Black;
}
public static void error(string texto){
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write(texto);
    Console.ForegroundColor = ConsoleColor.Black;
}

public static void menu(){
    string lectura;
    bool repetir;
    int y = 0;

    /*
    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.Black;
    */
    Console.Clear();

    string[] tabla_menu = new string[] {
    "╔═════════════════════════════════════════╗",
    "║           Servicios Bancarios           ║",
    "╠═════════════════════════════════════════╣",
    "║                                         ║",
    "║     1) Llegar a La Fila                 ║",
    "║     2) Asignar Fila                     ║",
    "║     3) Asignar Caja                     ║",
    "║     4) Liberar Caja                     ║",
    "║     5) Informacion                      ║",
    "║     6) Salir                            ║",
    "║                                         ║",
    "╚═════════════════════════════════════════╝"
    };

    for (int i = 0; i < tabla_menu.Length; i++){ printxy(5, y+=1, tabla_menu[i]);}

    printxy(1, y+=2, "Opcion: ");
    do {
        gotoxy(9, y);
        lectura = Console.ReadLine();
        repetir = false;

        switch (lectura){
            case "1":
                LLegarAFila();
            break;

            case "2":
                asignarFila();
            break;

            case "3":
                asignarCaja();
            break;

            case "4":
                LiberarCaja();
            break;

            case "5":
                verInformacion();
            break;

            case "6":
                Console.Clear();
                printxy(15,3,"Apagando Programa... \n\n");
                printxy(10,6,"Presione Enter Para Continuar... ");
                Console.ReadKey();
                Environment.Exit(0);
            break;

            default:
                repetir = true;
                printxy(9, y, new String(' ', lectura.Length));
            break;
        }
    } while (repetir);
    menu();
}

public static void LLegarAFila(){
    Console.Clear();
    string cliente, resp;
    int y=0, numAsignado;
    gotoxy(10,y); titulo("Llegar A La Fila");

    printxy(5, y+=2, "Nombre Del Cliente: ");
    gotoxy(25, 2); cliente = Console.ReadLine();

    if (cliente.Length < 3){
        gotoxy(7, y+=2); error("Nombre Invalido...");
        Console.ReadKey();
        LLegarAFila();
    }

    numAsignado = cliente_contador;
    cliente_contador++;

    printxy(5, y+=2, $"Numero Asignado: {numAsignado}");

    llegada.agregarAlFinal(cliente, numAsignado);

    y+=2;

    do{
        printxy(0, y, "¿Agregar otro cliente? s/n:");
        gotoxy(28, y);
        resp = Console.ReadLine().ToUpper();
        printxy(28, y, new String(' ', resp.Length));
    } while (resp != "S" && resp != "N");

    if (resp == "S"){
        LLegarAFila();
    } else {
        menu();
    }
}

public static void asignarFila(){
    Console.Clear();
    string resp;
    int y=0;
    object cliente, numAsignado;

    gotoxy(10,y); titulo("Asignar a Fila");

    if (llegada.contador == 0){
        gotoxy(2, y+=2); error("Actualmente No hay clientes en la fila");
        printxy(5, y+=2, "Presione Enter Para Continuar...");
        Console.ReadKey();
        menu();
    }

    llegada.retornarPrimero(2, y);

    printxy(5, y+=6, "Tipo De Fila: ");

    printxy(5, y+2, "1) Normal");
    printxy(5, y+3, "2) VIP");

    do{
        gotoxy(19, y);
        resp = Console.ReadLine().ToUpper();
        printxy(19, y, new String(' ', resp.Length));
    } while (resp != "1" && resp != "2");

    printxy(5, y+2, new String(' ', 10));
    printxy(5, y+3, new String(' ', 10));

    if (resp == "1"){
        printxy(19, y, "Normal");
        (cliente, numAsignado) = llegada.eliminarFrente();
        cola_Normal.agregarAlFinal(cliente, numAsignado);
    } else {
        printxy(19, y, "VIP");
        (cliente, numAsignado) = llegada.eliminarFrente();
        cola_VIP.agregarAlFinal(cliente, numAsignado);
    }

    y+=2;

    do{
        printxy(0, y, "¿Asignar otra fila? s/n:");
        gotoxy(25, y);
        resp = Console.ReadLine().ToUpper();
        printxy(25, y, new String(' ', resp.Length));
    } while (resp != "S" && resp != "N");

    if (resp == "S"){
        asignarFila();
    } else {
        menu();
    }
}

public static void asignarCaja(){
    Console.Clear();
    string resp;
    int y=0;
    object cliente, numAsignado;

    gotoxy(10,y); titulo("Asignar Caja");

    if (cola_Normal.contador == 0 && cola_VIP.contador == 0){
        gotoxy(2, y+=2); error("Actualmente No hay clientes en las filas");
        printxy(5, y+=2, "Presione Enter Para Continuar...");
        Console.ReadKey();
        menu();
    } else {
        printxy(5, y+=2, "1) Normal");
        printxy(5, y+=1, "2) VIP");

        printxy(5, y+=2, "opcion: ");

        do{
            gotoxy(13, y);
            resp = Console.ReadLine().ToUpper();
            printxy(13, y, new String(' ', resp.Length));
        } while (resp != "1" && resp != "2");

        if (resp == "1") asignarTipo(resp);
        else if (resp == "2") asignarTipo(resp);
        else {
            gotoxy(2, y+=2); error("Actualmente No hay clientes en la fila");
            printxy(5, y+=2, "Presione Enter Para Continuar...");
            Console.ReadKey();
            menu();
        }
    }
}

public static void asignarTipo(string tipo){
    string resp, cajaElegida;
    int y=0, disponibles, caja;
    bool repetir;
    object cliente, numAsignado;

    Console.Clear();

    if(tipo == "1"){
        gotoxy(10,y); titulo("Asignar A Caja Normal");
        cola_Normal.retornarPrimero(0,y);
        printxy(0, y+=8, "Cajas disponibles:");
        disponibles = cajas_Normales.enlistarDisponibles(0, y, asignado_caja_Normal);

        if (disponibles == 0){
            gotoxy(2, y+=2); error("Actualmente No hay cajas disponibles");
            printxy(5, y+=2, "Presione Enter Para Continuar...");
            Console.ReadKey();
            menu();
        }

        printxy(0, y-=2, $"Seleccione Caja: ");
        do{
            repetir = false;
            gotoxy(17,y); cajaElegida = Console.ReadLine();
            printxy(17, y, new String(' ', cajaElegida.Length));

            if(!int.TryParse(cajaElegida, out caja)) repetir = true;
            if(asignado_caja_Normal.cajaOcupada(caja)) repetir = true;
        } while (repetir);
        printxy(17, y, $"{caja}");
        printxy(0, y+2, new String(' ', 25));
        printxy(0, y+3, new String(' ', 25));
        printxy(0, y+4, new String(' ', 25));
        printxy(0, y+5, new String(' ', 25));
        (cliente, numAsignado) = cola_Normal.eliminarFrente();
        asignado_caja_Normal.agregarAlFinal(cliente, numAsignado);
        asignado_caja_Normal.primero.caja=caja;

    } else {
        gotoxy(10,y); titulo("Asignar A Caja VIP");
        cola_VIP.retornarPrimero(0,y);
        printxy(0, y+=8, "Cajas disponibles:");
        disponibles = cajas_VIP.enlistarDisponibles(0, y, asignado_caja_VIP);

        if (disponibles == 0){
            gotoxy(2, y+=2); error("Actualmente No hay cajas disponibles");
            printxy(5, y+=2, "Presione Enter Para Continuar...");
            Console.ReadKey();
            menu();
        }

        printxy(0, y-=2, $"Seleccione Caja: ");
        do{
            repetir = false;
            gotoxy(17,y); cajaElegida = Console.ReadLine();
            printxy(17, y, new String(' ', cajaElegida.Length));

            if(!int.TryParse(cajaElegida, out caja)) repetir = true;
            if(asignado_caja_VIP.cajaOcupada(caja)) repetir = true;
        } while (repetir);
        printxy(17, y, $"{caja}");
        printxy(0, y+2, new String(' ', 25));
        printxy(0, y+3, new String(' ', 25));
        printxy(0, y+4, new String(' ', 25));
        printxy(0, y+5, new String(' ', 25));
        (cliente, numAsignado) = cola_VIP.eliminarFrente();
        asignado_caja_VIP.agregarAlFinal(cliente, numAsignado);
        asignado_caja_VIP.primero.caja=caja;
        }

    printxy(0, y+=2, $"Cliente Asignado a La Caja {caja}");
    

    y+=2;
    do{
        printxy(0, y, "¿Asignar otra fila? s/n:");
        gotoxy(25, y);
        resp = Console.ReadLine().ToUpper();
        printxy(25, y, new String(' ', resp.Length));
    } while (resp != "S" && resp != "N");

    if (resp == "S"){
        asignarCaja();
    } else {
        menu();
    }
}

public static void LiberarCaja(){
    Console.Clear();
    string resp;
    int y=0;
    object cliente, numAsignado;

    gotoxy(10,y); titulo("Liberar Caja");

    
        printxy(5, y+=2, "1) Normal");
        printxy(5, y+=1, "2) VIP");

        printxy(5, y+=2, "opcion: ");

        do{
            gotoxy(13, y);
            resp = Console.ReadLine().ToUpper();
            printxy(13, y, new String(' ', resp.Length));
        } while (resp != "1" && resp != "2");

        liberarTipo(resp);
}

public static void liberarTipo(string tipo){
    string resp, cajaElegida;
    int y=0, disponibles, caja, ocupadas = 1;
    bool repetir;
    object cliente, numAsignado;

    Console.Clear();

    if(tipo == "1"){
        gotoxy(10,y); titulo("Liberar Caja Normal");
        printxy(0, y+=4, "Cajas Ocupadas:");
        ocupadas = cajas_Normales.enlistarOcupadas(0, y, asignado_caja_Normal);

        if (ocupadas == 0){
            gotoxy(2, y+=2); error("Actualmente No hay cajas Ocupadas");
            printxy(5, y+=2, "Presione Enter Para Continuar...");
            Console.ReadKey();
            menu();
        }

        printxy(0, y-=2, $"Seleccione Caja: ");
        do{
            repetir = false;
            gotoxy(17,y); cajaElegida = Console.ReadLine();
            printxy(17, y, new String(' ', cajaElegida.Length));

            if(!int.TryParse(cajaElegida, out caja)) repetir = true;
            if(!asignado_caja_Normal.cajaOcupada(caja)) repetir = true;
            if (caja > cajas_Normales.contador || caja <= 0 ) repetir = true;
        } while (repetir);
        printxy(17, y, $"{caja}");
        printxy(0, y+2, new String(' ', 25));
        printxy(0, y+3, new String(' ', 25));
        printxy(0, y+4, new String(' ', 25));
        printxy(0, y+5, new String(' ', 25));

        do{
            printxy(0, y, $"¿Liberar caja {caja}? s/n:");
            gotoxy(28, y);
            resp = Console.ReadLine().ToUpper();
            printxy(28, y, new String(' ', resp.Length));
        } while (resp != "S" && resp != "N");

        if (resp == "S"){
            asignado_caja_Normal.eliminarPorValor(caja);
            
            printxy(0, y+=2, $"Caja {caja} Eliminada");
        }

    } else if (tipo=="2"){
        gotoxy(10,y); titulo("Liberar A Caja VIP");
        printxy(0, y+=4, "Cajas Ocupadas:");
        ocupadas = cajas_VIP.enlistarOcupadas(0, y, asignado_caja_VIP);

        printxy(0, y-=2, $"Seleccione Caja: ");
        do{
            repetir = false;
            gotoxy(17,y); cajaElegida = Console.ReadLine();
            printxy(17, y, new String(' ', cajaElegida.Length));

            if(!int.TryParse(cajaElegida, out caja)) repetir = true;
            if(!asignado_caja_VIP.cajaOcupada(caja)) repetir = true;
            if (caja > cajas_VIP.contador || caja <= 0 ) repetir = true;
        } while (repetir);
        printxy(17, y, $"{caja}");
        printxy(0, y+2, new String(' ', 25));
        printxy(0, y+3, new String(' ', 25));
        printxy(0, y+4, new String(' ', 25));
        printxy(0, y+5, new String(' ', 25));

y+=2;
        do{
            printxy(0, y, $"¿Liberar caja {caja}? s/n:");
            gotoxy(28, y);
            resp = Console.ReadLine().ToUpper();
            printxy(28, y, new String(' ', resp.Length));
        } while (resp != "S" && resp != "N");

        if (resp == "S"){
            asignado_caja_VIP.eliminarPorValor(caja);
            
            printxy(0, y+=2, $"Caja {caja} Liberadas");
        }
    }

    y+=2;
    do{
        printxy(0, y, "¿Liberar otra Caja? s/n:");
        gotoxy(25, y);
        resp = Console.ReadLine().ToUpper();
        printxy(25, y, new String(' ', resp.Length));
    } while (resp != "S" && resp != "N");

    if (resp == "S"){
        LiberarCaja();
    } else {
        menu();
    }
}


public static void verInformacion() {
    string opcion;
    do {
        Console.Clear();
        int y = 0;
        gotoxy(10, y); titulo("Información");
        printxy(5, y += 2, "1) Clientes");
        printxy(5, y += 1, "2) Cajas");
        printxy(5, y += 1, "3) Regresar");
        printxy(5, y += 2, "Opción: ");
        
        gotoxy(13, y);
        opcion = Console.ReadLine()?.Trim();
        
        switch (opcion) {
            case "1":
                mostrarClientes();
                break;
            case "2":
                mostrarCajas();
                break;
        }
    } while (opcion != "3");
}

public static void mostrarClientes(){
    int y = 0;
    string resp;

    Console.Clear();
    titulo("Información de Clientes");

    gotoxy(0, y+=2); titulo("Esperando a ser asignados:");
    printxy(0, y+1, "Número Nombre");
    llegada.enlistar(2, y+2);

    gotoxy(35, y); titulo("Esperando en caja normal:");
    printxy(35, y+1, "Número Nombre");
    cola_Normal.enlistar(37, y+2);

    gotoxy(70, y); titulo("Esperando en caja VIP:");
    printxy(70, y+1, "Número Nombre");
    cola_VIP.enlistar(72, y+2);

    printxy(5, y += 25, "Presiona enter para continuar...");
    Console.ReadKey();
}

public static void mostrarCajas(){
    int y = 0;
    string resp;

    Console.Clear();
    titulo("Información de Cajas");
    printxy(0,2, "Cajas Normales");
    asignado_caja_Normal.enlistarCajas(0,2);


    printxy(0, 7, "Cajas VIP");
    asignado_caja_VIP.enlistarCajas(0, 7);

    printxy(5, y+=20, "Presiona enter para continuar...");
    Console.ReadKey();
}


public static void printxy(int x, int y, string mensaje){
        Console.SetCursorPosition(x,y);
        Console.Write(mensaje);
    }

    public static void gotoxy(int x, int y){
        Console.SetCursorPosition(x,y);
    } // go to xy



} //Clase Program

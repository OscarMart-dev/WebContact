const notificacion = new AWN();

function notificar(tipo){

    switch (tipo) {

        case 'success':
            notificacion.success('Contacto Creado')
            break;

        case 'info':
            notificacion.info('No hay registros para editar')
            break;

        case 'alert':
            notificacion.alert('alerta amarillo')
            break;

        case 'warning':
            notificacion.warning('El contacto ya existe')
            break;

        case 'obligId':
            notificacion.alert('El N Identificacion es obligatorio')
            break;

        case 'obligNom':
            notificacion.alert('El Nombre es obligatorio')
            break;

        case 'obligMov':
            notificacion.alert('El Movil es obligatorio')
            break;

        case 'delexist':
            notificacion.alert('No hay contacto para borrar')
            break;

        case 'delete':
            notificacion.warning('Contacto Eliminado')
            break;

        default:

            break;

    }


}
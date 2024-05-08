const notificacion = new AWN();

function notificar(tipo){

    switch (tipo) {

        case 'success':
            notificacion.success('bien verde')
            break;

        case 'info':
            notificacion.info('info azul')
            break;

        case 'alert':
            notificacion.alert('alerta amarillo')
            break;

        case 'warning':
            notificacion.warning('El contacto ya existe')
            break;

        case 'obligId':
            notificacion.alert('El N° Identificación es obligatorio')
            break;

        case 'obligNom':
            notificacion.alert('El Nombre es obligatorio')
            break;

        case 'obligMov':
            notificacion.alert('El Móvil es obligatorio')
            break;

        default:

            break;

    }

}
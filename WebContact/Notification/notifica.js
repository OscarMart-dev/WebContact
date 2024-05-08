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
            notificacion.warning('warning rojo')
            break;

        default:

            break;

    }

}
import { ErrorHandler, Injectable } from '@angular/core';
import Swal from 'sweetalert2';
@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor() {}
  handleError(error) {
    let text = '';
    const body = JSON.parse(error._body);
    Object.keys(body).forEach(k => {
      text += `<p style="text-align:left;"><b>${k}:</b> ${body[k]}</p>`;
    });

    Swal.fire({
      title: error.statusText,
      html: text,
      type: 'error',
      confirmButtonText: 'OK'
    });
  }
}

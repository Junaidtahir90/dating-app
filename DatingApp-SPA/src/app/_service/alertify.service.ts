import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }

// Notifcation Library https://alertifyjs.com/
confirm(message: string, okCallBack: () => any ) {
  alertify.cofirm(message, function(e) {
    if(e) {
      okCallBack();
    }
    else{
      // Do nothing
    }
  });
}

success(message: string){
  alertify.success(message);
}

error(message: string) {
  alertify.error(message);
}

warning(message: string) {
  alertify.warning(message);
}


message(message: string) {
  alertify.message(message);
}

}

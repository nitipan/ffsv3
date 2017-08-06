import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Rx';

export function toDataURL(url): Observable<any> {
    let subject = new Subject<any>();
    var xhr = new XMLHttpRequest();
    xhr.onload = function () {
        var reader = new FileReader();
        reader.onloadend = function () {
            subject.next(reader.result);
        }
        reader.readAsDataURL(xhr.response);
    };
    xhr.open('GET', url);
    xhr.responseType = 'blob';
    xhr.send();

    return subject.asObservable();
}
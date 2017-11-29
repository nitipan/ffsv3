import { Http } from '@angular/http';
import { KV } from './../model/kv';
import { Observable } from 'rxjs/Rx';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'asynckv' })
export class AsyncKVPipe implements PipeTransform {
    constructor(private http: Http) {

    }
    transform(value: string, k: string, v: string): Observable<KV[]> {
        console.log(value, k, v);
        return this.http.get(value)
            .map(response => response.json() as any[])
            .map(arr => arr.map(a => { return { key: a[k], value: a[v] }; }));
    }
}
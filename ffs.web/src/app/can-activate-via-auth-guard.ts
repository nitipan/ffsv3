import { Router } from '@angular/router';
import { EventService } from './event.service';
import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';


@Injectable()
export class CanActivateViaAuthGuard implements CanActivate {

    constructor(private eventService: EventService, private router: Router) { }

    canActivate() {
        var user = localStorage.getItem("user");
        var isLogged = user != undefined && user != '' && user != null;

        if (!isLogged) {
            this.router.navigateByUrl('/home');
            this.eventService.requestLogin.next(null);
        }
        return isLogged;
    }
}
import { CanActivateViaAuthGuard } from './can-activate-via-auth-guard';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { InputBase } from './model/inputbase';
import { EventService } from './event.service';
import { Component, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { ModuleBase } from "./modules/module-base.component";
import { Router, ActivatedRoute, ResolveEnd } from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [
    trigger('loginSlideInOut', [
      state('in', style({
        transform: 'translate3d(0, 0, 0)',
        opacity: 1,
        position: 'fixed',
        top: 0,
        left: 0,
        right: 'auto',
        margin: 0,
        padding: 0,
        zIndex: 9999,
        height: '100%'
      })),
      state('out', style({
        transform: 'translate3d(-100%, 0, 0)',
        opacity: 0,
        display: 'none'
      })),
      transition('in => out', animate('200ms ease-in-out')),
      transition('out => in', animate('200ms ease-in-out'))
    ]), trigger('mainSlideInOut', [
      state('in', style({
        marginLeft: '25%'
      })),
      state('out', style({
        marginLeft: '0'
      })),
      transition('in => out', animate('200ms ease-in-out')),
      transition('out => in', animate('200ms ease-in-out'))
    ])
  ]
})
export class AppComponent implements AfterViewInit {

  commonInput: InputBase = null;
  currentModule: ModuleBase = null;

  state = 'out';

  user: any;

  routeUrl: any;
  constructor(private eventService: EventService, private router: Router, private canActivateViaAuthGuard: CanActivateViaAuthGuard, private route: ActivatedRoute) {
    this.router.events
      .filter(e => e instanceof ResolveEnd)
      .subscribe((e: ResolveEnd) => {
        this.routeUrl = e.url;
      });
    this.eventService.afterLogin.subscribe((u) => {
      localStorage.setItem("user", JSON.stringify(u));
      this.user = u;
      this.state = 'out';
    });

    if (!this.canActivateViaAuthGuard.canActivate()) {
      this.state = 'in';
    } else {
      this.state = 'out';
      this.user = JSON.parse(localStorage.getItem('user'));
    }
  }

  ngAfterViewInit(): void {
    this.eventService.calculationModuleSubject.subscribe(c => {
      if (c == null)
        this.commonInput = null;

      this.currentModule = c;

      if (this.currentModule != null) {
        this.currentModule.valueChanges.subscribe(m => {
          this.commonInput = m;
        });

      }

    });
  }

  logout() {
    this.router.navigateByUrl('/home');
    this.user = undefined;
    localStorage.removeItem("user");
    this.state = 'in';
    this.eventService.requestLogin.next(null);
  }
}

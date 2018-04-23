import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Session } from '../models/session';
import { Observable } from 'rxjs/Observable';
import { SessionsService } from '../services/sessions.service';
import { Injectable } from '@angular/core';

@Injectable()
export class SessionResolver implements Resolve<Session> {
  constructor(private readonly _sessionsService: SessionsService) {}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Session | Observable<Session> | Promise<Session> {
    const sessionId = route.params.id;
    if (!sessionId) {
      return new Session();
    }
    return this._sessionsService.getSessionById(sessionId);
  }
}

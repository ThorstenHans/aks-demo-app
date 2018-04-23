import { SessionsService } from '../sessions.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Session } from '../../models/session';
import { Observable } from 'rxjs/Observable';
import { mapTo } from 'rxjs/operators';

@Injectable()
export class SessionsServiceRef extends SessionsService {
  constructor(private readonly _http: HttpClient) {
    super();
  }

  public getAllSessions(): Observable<Array<Session>> {
    return this._http.get<Array<Session>>('api/sessions');
  }
  public getSessionById(sessionId: string): Observable<Session> {
    return this._http.get<Session>(`api/sessions/${sessionId}`);
  }

  public deleteSession(sessionId: string): Observable<any> {
    return this._http.delete(`api/sessions/${sessionId}`);
  }

  public updateSession(session: Session): Observable<Session> {
    return this._http.put<Session>(`api/sessions/${session.id}`, session);
  }
  public createSession(session: Session): Observable<Session> {
    return this._http.post<Session>(`api/sessions`, session);
  }
}

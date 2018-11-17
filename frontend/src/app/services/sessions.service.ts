import { Session } from '../models/session';
import { Observable } from 'rxjs';

export abstract class SessionsService {
  public abstract getAllSessions(): Observable<Array<Session>>;
  public abstract getSessionById(sessionId: string): Observable<Session>;
  public abstract createSession(session: Session): Observable<Session>;
  public abstract updateSession(session: Session): Observable<Session>;
  public abstract deleteSession(sessionId: string): Observable<any>;
}

import { SessionsService } from '../sessions.service';
import { Observable, of } from 'rxjs';
import { Session } from '../../models/session';

export class SessionsServiceMock extends SessionsService {
    private readonly _sessions = [];
    constructor() {
        super();

        this._sessions.push({ id: '1', title: 'Session A', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 0 } as Session);
        this._sessions.push({ id: '2', title: 'Session B', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 1 } as Session);
        this._sessions.push({ id: '3', title: 'Session C', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 3 } as Session);
        this._sessions.push({ id: '4', title: 'Session D', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 4 } as Session);
        this._sessions.push({ id: '5', title: 'Session E', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 5 } as Session);
        this._sessions.push({ id: '6', title: 'Session F', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 6 } as Session);
        this._sessions.push({ id: '7', title: 'Session G', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 7 } as Session);
        this._sessions.push({ id: '8', title: 'Session H', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 8 } as Session);
        this._sessions.push({ id: '9', title: 'Session I', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 9 } as Session);
        this._sessions.push({ id: '10', title: 'Session J', abstract: 'FooBar will be shown in this talk', speaker: 'Thorsten Hans', conference: 'API Summit', level: 200, audience: 0, downVotes: 0, upVotes: 10 } as Session);
    }
    public getAllSessions(): Observable<Session[]> {
        return of(this._sessions);
    }
    public getSessionById(sessionId: string): Observable<Session> {
        const found = this._sessions.find(s => s.id === sessionId);
        return of(found);
    }
    public createSession(session: Session): Observable<Session> {
        session.id = this._sessions.length.toString();
        this._sessions.push(session);
        return of(session);

    }
    public updateSession(session: Session): Observable<Session> {
        let found = this._sessions.find(s => s.id === session.id);
        if (found) {
            found = session;
        }
        return of(found);

    }
    public deleteSession(sessionId: string): Observable<any> {
        const found = this._sessions.find(s => s.id === sessionId);
        if (found) {
            const idx = this._sessions.indexOf(found);
            this._sessions.splice(idx, 1);
            return of(true);
        }
        return of(false);
    }


}
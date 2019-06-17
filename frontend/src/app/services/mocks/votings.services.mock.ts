import { VotingsService } from '../votings.service';
import { Observable, of } from 'rxjs';
import { VotingSummary } from '../../models/votingSummary';
import { tap, map } from 'rxjs/operators';


export class VotingsServiceMock extends VotingsService {
    private _votings = {};
    constructor() {
        super();
    }

    public getVotingSummary(sessionId: string): Observable<VotingSummary> {
        if (!this._votings.hasOwnProperty(sessionId)) {
            this._votings[sessionId] = { sessionId: sessionId, upVotes: 2, downVotes: 1 } as VotingSummary;
        }
        return of(this._votings[sessionId]);
    }

    public voteUp(sessionId: string): Observable<boolean> {
        const voting = this._votings[sessionId];
        voting.upVotes = voting.upVotes + 1;
        this._votings[sessionId] = voting;
        return of(true);
        
    }
    public voteDown(sessionId: string): Observable<boolean> {
        const voting = this._votings[sessionId];
        voting.downVotes = voting.downVotes + 1;
        this._votings[sessionId] = voting;
        return of(true);
    }


}
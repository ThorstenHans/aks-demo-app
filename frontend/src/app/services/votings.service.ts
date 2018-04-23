import { Observable } from 'rxjs/Observable';
import { VotingSummary } from '../models/votingSummary';
import { Voting } from '../models/voting';
import { mapTo } from 'rxjs/operators';

export abstract class VotingsService {
  public abstract getVotingSummary(sessionId: string): Observable<VotingSummary>;
  public abstract voteUp(sessionId: string): Observable<boolean>;
  public abstract voteDown(sessionId: string): Observable<boolean>;
}

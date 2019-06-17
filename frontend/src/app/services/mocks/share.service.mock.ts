import { ShareService } from '../share.service';
import { Session } from '../../models/session';
import { Observable, of } from 'rxjs';

export class ShareServiceMock extends ShareService{
    constructor(){
        super();
    }
    
    public shareSession(target: string, session: Session): Observable<any> {
        return of(true);
    }

}
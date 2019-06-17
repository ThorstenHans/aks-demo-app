import { ExportService } from '../export.service';
import { Observable, of } from 'rxjs';

export class ExportServiceMock extends ExportService {
    
    constructor(){
        super();
    }

    public exportSession(sessionId: string): Observable<any> {
        return of(true);
    }
}
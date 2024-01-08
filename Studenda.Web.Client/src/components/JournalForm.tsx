import { FC } from 'react'
import JournalSubject from './UI/journalsubject/JournalSubject'


const JournalForm: FC = () => {

    return (
        <div style={{display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'10px', alignSelf: 'center'}}>
              <JournalSubject text='Предмет'/>
              <JournalSubject text='Предмет'/>
              <JournalSubject text='Предмет'/>
              <JournalSubject text='Predmet'/>
                
        </div>

    )
}

export default JournalForm
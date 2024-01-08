import { FC } from 'react'
import classes from './JournalSubject.module.css'

interface subjectProps {
        text?: string;
        onClick?: ()=> void
}
const JournalSubject: FC<subjectProps> = 
        ({text, onClick}) => {
   
    return (
                <div className={classes.elementBox} onClick={onClick}>
                        <div className={classes.subjectContainer}>
                                <p className={classes.text}>{text}</p>
                        </div>             
                </div>
    )
}

export default JournalSubject
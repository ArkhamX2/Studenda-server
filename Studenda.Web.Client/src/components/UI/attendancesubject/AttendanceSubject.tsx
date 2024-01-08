import { FC } from 'react'
import classes from './AttendanceSubject.module.css'

interface subjectProps {
        date?: string;
        time?: string;
        week?: string;
        visit?:string;
}
const AttendanceSubject: FC<subjectProps> = 
        ({date, time, week,visit}) => {
   
    return (
                <div className={classes.elementBox}>
                        <div className={classes.subjectContainer}>
                                <p className={classes.text}>{date}</p>
                                <p className={classes.text} style={{marginLeft:'8px'}}>{time}</p> 
                                <p className={classes.text} style={{marginLeft:'10px'}}>{week}</p> 
                                <b style={{ fontSize:'28px', marginLeft:'10px'}}>{visit}</b>
                        </div>   
                </div>
    )
}

export default AttendanceSubject
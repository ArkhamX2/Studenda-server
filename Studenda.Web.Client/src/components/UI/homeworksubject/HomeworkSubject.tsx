import { FC } from 'react'
import classes from './HomeworkSubject.module.css'

interface subjectProps {
        text?: string;
        grade?: string;
}
const HomeworkSubject: FC<subjectProps> = 
        ({text, grade}) => {
   
    return (
                <div className={classes.elementBox}>
                        <div className={classes.subjectContainer} >
                                <div className={classes.text}>{text}</div>
                                <div className={classes.text} style={{marginLeft:'16px', justifySelf:'right'}}>{grade}</div>
                        </div>             
                </div>
    )
}

export default HomeworkSubject
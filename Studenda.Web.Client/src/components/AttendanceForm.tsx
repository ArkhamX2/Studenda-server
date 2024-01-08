import { FC } from 'react'
import AttendanceSubject from './UI/attendancesubject/AttendanceSubject'

const AttendanceForm: FC = () => {

    return (
        <div style={{display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'10px', alignSelf: 'center'}}>
            <AttendanceSubject date='08.01' time='10:22' week='Неделя: красная' visit='+'/>
            <AttendanceSubject date='09.01' time='10:22' week='Неделя: красная' visit='−'/>
            <AttendanceSubject date='10.02' time='10:22' week='Неделя: синяя' visit='+'/>
                
        </div>

    )
}

export default AttendanceForm
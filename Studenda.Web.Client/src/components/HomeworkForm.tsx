import { FC } from 'react'
import HomeworkSubject from './UI/homeworksubject/HomeworkSubject'

const HomeworkForm: FC = () => {

    return (
        <div style={{display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'10px', alignSelf: 'center'}}>
            <div style={{display:'inline-flex',border:'2px solid lightgray',padding:'10px', borderRadius:'5px', margin:'5px'}}>
                <p style={{fontSize:'18px'}}>Посещаемость</p>
                <img></img>
            </div>
            <HomeworkSubject text='Лабораторная работа' grade='5'/>
            <HomeworkSubject text='Лабораторная работа' grade='5'/>
            <HomeworkSubject text='Лабораторная работа' grade='5'/>
                
        </div>

    )
}

export default HomeworkForm
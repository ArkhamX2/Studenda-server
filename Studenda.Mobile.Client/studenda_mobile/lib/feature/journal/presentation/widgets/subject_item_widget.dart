import 'package:flutter/material.dart';
import 'package:studenda_mobile/feature/journal/presentation/widgets/journal_subject_screen_widget.dart';
import 'package:studenda_mobile/feature/schedule/domain/entities/subject_entity.dart';
import 'package:studenda_mobile/resources/colors.dart';

class SubjectItemWidget extends StatelessWidget {
  final SubjectEntity subject;
  const SubjectItemWidget({
    super.key,
    required this.subject,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 5),
      child: GestureDetector(
        onTap: () => {
          Navigator.of(context).push(
            MaterialPageRoute(
              builder: (context) =>
                  JournalSubjectScreenWidget(subject: subject),
            ),
          ),
        },
        child: SizedBox(
          height: 50,
          child: Container(
            decoration: BoxDecoration(
              border: Border.all(
                color: const Color.fromARGB(255, 170, 141, 211),
              ),
              color: Colors.white,
              borderRadius: const BorderRadius.all(Radius.circular(5)),
            ),
            child: Center(
              child: Text(
                subject.name,
                style: const TextStyle(
                  color: mainForegroundColor,
                  fontSize: 16,
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}

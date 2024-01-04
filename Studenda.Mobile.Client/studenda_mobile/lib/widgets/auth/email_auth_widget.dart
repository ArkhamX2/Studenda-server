import 'package:flutter/material.dart';

class EmailAuthWidget extends StatefulWidget {
  const EmailAuthWidget({super.key});

  @override
  State<EmailAuthWidget> createState() => _EmailAuthWidgetState();
}

class _EmailAuthWidgetState extends State<EmailAuthWidget> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.chevron_left_sharp),
          onPressed: () => {},
        ),
        titleSpacing: 0,
        centerTitle: true,
        title: const Text(
          'Вход',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
      ),
      body: Container(
        alignment: AlignmentDirectional.center,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 17),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              const Text(
                "Введите свой email:",
                style: TextStyle(
                    color: Color.fromARGB(255, 56, 31, 118), fontSize: 20,),
              ),
              const SizedBox(
                height: 23,
              ),
              const TextField(),
              const SizedBox(
                height: 23,
              ),
              Center(
                child: ElevatedButton(
                  onPressed: () {},
                  style: ButtonStyle(
                    minimumSize: const MaterialStatePropertyAll(Size(300, 50)),
                    shape: MaterialStatePropertyAll(
                      RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(9),
                      ),
                    ),
                    backgroundColor: const MaterialStatePropertyAll(
                      Color.fromARGB(255, 231, 225, 255),
                    ),
                  ),
                  child: const Text(
                    "Получить код",
                    style: TextStyle(
                      color: Color.fromARGB(255, 101, 59, 159),
                      fontSize: 23,
                    ),
                  ),
                ),
              ),
              const SizedBox(
                height: 34,
              ),
            ],
          ),
        ),
      ),
    );
  }
}

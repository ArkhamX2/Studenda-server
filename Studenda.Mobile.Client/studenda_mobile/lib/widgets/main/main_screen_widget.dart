import 'package:flutter/material.dart';

class MainScreenWidget extends StatefulWidget {
  const MainScreenWidget({super.key});

  @override
  State<MainScreenWidget> createState() => _MainScreenWidgetState();
}

class _MainScreenWidgetState extends State<MainScreenWidget> {
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
          'Главная',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
        actions: [
          IconButton(
              onPressed: () => {}, icon: const Icon(Icons.notifications),),
        ],
      ),
      body: Container(
        alignment: AlignmentDirectional.center,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 17),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const _ScheduleItemWidget(),
              ElevatedButton(
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
                  "Подтвердить",
                  style: TextStyle(
                    color: Color.fromARGB(255, 101, 59, 159),
                    fontSize: 23,
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class _ScheduleItemWidget extends StatelessWidget {
  const _ScheduleItemWidget();

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: IntrinsicHeight(
        child: Row(
          children: [
            const Column(
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                Text("8:30"),
                Text("10:15"),
              ],
            ),
            const VerticalDivider(
              width: 20,
              thickness: 1,
              indent: 5,
              endIndent: 5,
              color: Colors.grey,
            ),
            Expanded(
              child: Container(
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(5),
                  border: Border.all(
                    color: const Color.fromARGB(60, 0, 0, 0),
                  ),
                ),
                child: const Row(
                  children: [
                    Expanded(child: Center(child: Text("Ц-150"))),
                    Text("Ц-150"),
                    SizedBox(
                      width: 14,
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
